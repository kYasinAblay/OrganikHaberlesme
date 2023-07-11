using OrganikHaberlesme.Application.DTOs.Common;

namespace OrganikHaberlesme.Application.DTOs.LeaveRequest
{
    public class ChangeLeaveRequestApprovalDto : BaseDto
    {
        public bool Approved { get; set; }
    }
}
