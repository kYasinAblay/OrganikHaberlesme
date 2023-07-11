using OrganikHaberlesme.Application.DTOs.LeaveType;

using MediatR;

namespace OrganikHaberlesme.Application.Features.LeaveTypes.Requests.Queries
{
    public class GetLeaveTypeDetailRequest : IRequest<LeaveTypeDto>
    {
        public int Id { get; set; }
    }
}
