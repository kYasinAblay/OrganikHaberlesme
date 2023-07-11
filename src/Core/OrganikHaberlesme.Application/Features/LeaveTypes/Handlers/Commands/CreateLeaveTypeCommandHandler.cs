using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using OrganikHaberlesme.Application.Contracts.Persistence;
using OrganikHaberlesme.Application.DTOs.LeaveType.Validators;
using OrganikHaberlesme.Application.Features.LeaveTypes.Requests.Commands;
using OrganikHaberlesme.Application.Responses;
using OrganikHaberlesme.Domain;

using MediatR;

namespace OrganikHaberlesme.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateLeaveTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed.";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var leaveType = _mapper.Map<LeaveType>(request.CreateLeaveTypeDto);
                leaveType = await _leaveTypeRepository.Add(leaveType);

                response.Success = true;
                response.Message = "Creation Successful.";
                response.Id = leaveType.Id;
            }

            return response;
        }
    }
}
