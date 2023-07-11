using OrganikHaberlesme.Application.DTOs.Common;

namespace OrganikHaberlesme.Application.DTOs.LeaveType
{
    public class LeaveTypeDto : BaseDto, ILeaveTypeDto
    {
        public string Name { get; set; }

        public int DefaultDays { get; set; }
    }
}
