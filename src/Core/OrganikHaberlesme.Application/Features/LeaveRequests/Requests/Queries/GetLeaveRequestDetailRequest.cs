using OrganikHaberlesme.Application.DTOs.LeaveRequest;

using MediatR;

namespace OrganikHaberlesme.Application.Features.LeaveRequests.Requests.Queries
{
    public class GetLeaveRequestDetailRequest : IRequest<LeaveRequestDto>
    {
        public int Id { get; set; }
    }
}
