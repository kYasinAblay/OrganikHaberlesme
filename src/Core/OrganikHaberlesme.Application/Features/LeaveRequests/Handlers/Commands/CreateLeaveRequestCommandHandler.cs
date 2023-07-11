using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using OrganikHaberlesme.Application.Contracts.Infrastructure;
using OrganikHaberlesme.Application.Contracts.Persistence;
using OrganikHaberlesme.Application.DTOs.LeaveRequest.Validators;
using OrganikHaberlesme.Application.Features.LeaveRequests.Requests.Commands;
using OrganikHaberlesme.Application.Models.Email;
using OrganikHaberlesme.Application.Responses;
using OrganikHaberlesme.Domain;

using MediatR;

using Microsoft.AspNetCore.Http;

namespace OrganikHaberlesme.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreateLeaveRequestCommandHandler(
            IUnitOfWork unitOfWork,
            IEmailSender emailSender,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestDtoValidator(_unitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto, cancellationToken);
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "uid")?.Value;
            var allocation = await _unitOfWork.LeaveAllocationRepository.GetUserAllocations(userId, request.LeaveRequestDto.LeaveTypeId);
            var daysRequested = (int)(request.LeaveRequestDto.EndDate - request.LeaveRequestDto.StartDate).TotalDays;

            if (daysRequested > allocation?.NumberOfDays)
            {
                validationResult.Errors.Add(new FluentValidation
                    .Results
                    .ValidationFailure(nameof(request.LeaveRequestDto.EndDate), "You do not have enough days for this request."));
            }

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            try
            {

                var leaveRequest = this._mapper.Map<LeaveRequest>(request.LeaveRequestDto);

                leaveRequest = await _unitOfWork.LeaveRequestRepository.Add(leaveRequest);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = leaveRequest.Id;

                var emailAddress = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                var email = new Email();

                if (!string.IsNullOrEmpty(emailAddress))
                {
                    email.To = emailAddress;
                    email.Body = $"Your leave request from {request.LeaveRequestDto.StartDate:D} to {request.LeaveRequestDto.EndDate:D} has been submitted successfuly.";
                    email.Subject = "Leave Request Submitted";
                }
                else
                {
                    throw new Exception();
                }

                await _emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                // TODO
            }

            return response;
        }
    }
}
