using OrganikHaberlesme.Application.DTOs.LeaveAllocation;

using MediatR;

namespace OrganikHaberlesme.Application.Features.LeaveAllocations.Requests.Commands
{
    public class UpdateLeaveAllocationCommand : IRequest<Unit>
    {
        public UpdateLeaveAllocationDto LeaveAllocationDto { get; set; }
    }
}
