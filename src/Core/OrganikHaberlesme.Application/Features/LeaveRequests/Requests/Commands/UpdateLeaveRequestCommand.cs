using OrganikHaberlesme.Application.DTOs.LeaveRequest;

using MediatR;

namespace OrganikHaberlesme.Application.Features.LeaveRequests.Requests.Commands
{
    public class UpdateLeaveRequestCommand : IRequest<Unit>
    {
        public int Id { get; set; }

        public UpdateLeaveRequestDto UpdateLeaveRequestDto { get; set; }

        public ChangeLeaveRequestApprovalDto ChangeLeaveRequestApprovalDto { get; set; }
    }
}
