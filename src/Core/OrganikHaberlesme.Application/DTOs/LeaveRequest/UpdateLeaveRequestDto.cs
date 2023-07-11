using System;

using OrganikHaberlesme.Application.DTOs.Common;

namespace OrganikHaberlesme.Application.DTOs.LeaveRequest
{
    public class UpdateLeaveRequestDto : BaseDto, ILeaveRequestDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int LeaveTypeId { get; set; }

        public string RequestComments { get; set; }

        public bool Cancelled { get; set; }
    }
}
