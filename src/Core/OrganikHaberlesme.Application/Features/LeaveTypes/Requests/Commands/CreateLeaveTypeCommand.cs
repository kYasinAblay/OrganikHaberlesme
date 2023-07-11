using OrganikHaberlesme.Application.DTOs.LeaveType;
using OrganikHaberlesme.Application.Responses;

using MediatR;

namespace OrganikHaberlesme.Application.Features.LeaveTypes.Requests.Commands
{
    public class CreateLeaveTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateLeaveTypeDto CreateLeaveTypeDto { get; set; }
    }
}
