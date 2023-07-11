using OrganikHaberlesme.Application.DTOs.LeaveAllocation;

using MediatR;

namespace OrganikHaberlesme.Application.Features.LeaveAllocations.Requests.Queries
{
    public class GetLeaveAllocationDetailRequest : IRequest<LeaveAllocationDto>
    {
        public int Id { get; set; }
    }
}
