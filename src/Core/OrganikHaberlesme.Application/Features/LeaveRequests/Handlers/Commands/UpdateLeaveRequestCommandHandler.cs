using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using FluentValidation;

using OrganikHaberlesme.Application.Contracts.Persistence;
using OrganikHaberlesme.Application.DTOs.LeaveRequest.Validators;
using OrganikHaberlesme.Application.Features.LeaveRequests.Requests.Commands;

using MediatR;

namespace OrganikHaberlesme.Application.Features.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _unitOfWork.LeaveRequestRepository.Get(request.Id);

            if (request.UpdateLeaveRequestDto != null)
            {
                var validator = new UpdateLeaveRequestDtoValidator(_unitOfWork.LeaveTypeRepository);
                var validationResult = await validator.ValidateAsync(request.UpdateLeaveRequestDto, cancellationToken);
                if (validationResult.IsValid == false)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                _mapper.Map(request.UpdateLeaveRequestDto, leaveRequest);

                await _unitOfWork.LeaveRequestRepository.Update(leaveRequest);
                await _unitOfWork.Save();
            }
            else if (request.ChangeLeaveRequestApprovalDto != null)
            {
                await _unitOfWork.LeaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDto.Approved);

                if (request.ChangeLeaveRequestApprovalDto.Approved)
                {
                    var allocation = await _unitOfWork.LeaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                    var daysRequested = (int)(request.UpdateLeaveRequestDto.EndDate - request.UpdateLeaveRequestDto.StartDate).TotalDays;

                    allocation.NumberOfDays -= daysRequested;

                    await _unitOfWork.LeaveAllocationRepository.Update(allocation);
                    await _unitOfWork.Save();
                }
            }

            return Unit.Value;
        }
    }
}
