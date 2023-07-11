using System.Collections.Generic;

using OrganikHaberlesme.Application.DTOs.LeaveRequest;

using MediatR;

namespace OrganikHaberlesme.Application.Features.LeaveRequests.Requests.Queries
{
    public class GetLeaveRequestListRequest : IRequest<List<LeaveRequestListDto>>
    {
        public bool IsLoggedInUser { get; set; }
    }
}
