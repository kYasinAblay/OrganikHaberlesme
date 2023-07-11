using System.Collections.Generic;

using OrganikHaberlesme.Application.DTOs.LeaveAllocation;

using MediatR;

namespace OrganikHaberlesme.Application.Features.LeaveAllocations.Requests.Queries
{
    public class GetLeaveAllocationListRequest : IRequest<List<LeaveAllocationDto>>
    {
        public bool IsLoggedInUser { get; set; }
    }
}
