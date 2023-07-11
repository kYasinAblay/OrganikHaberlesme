using OrganikHaberlesme.Application.DTOs.LeaveAllocation;
using OrganikHaberlesme.Application.Responses;

using MediatR;

namespace OrganikHaberlesme.Application.Features.LeaveAllocations.Requests.Commands
{
    public class CreateLeaveAllocationCommand : IRequest<BaseCommandResponse>
    {
        public CreateLeaveAllocationDto LeaveAllocationDto { get; set; }
    }
}
