using OrganikHaberlesme.Application.DTOs.LeaveType;

using MediatR;

namespace OrganikHaberlesme.Application.Features.LeaveTypes.Requests.Commands
{
    public class UpdateLeaveTypeCommand : IRequest<Unit>
    {
        public LeaveTypeDto LeaveTypeDto { get; set; }
    }
}
