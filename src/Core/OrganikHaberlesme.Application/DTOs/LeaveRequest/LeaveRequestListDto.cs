using System;

using OrganikHaberlesme.Application.DTOs.Common;
using OrganikHaberlesme.Application.DTOs.LeaveType;
using OrganikHaberlesme.Application.Models.Identity;

namespace OrganikHaberlesme.Application.DTOs.LeaveRequest
{
    public class LeaveRequestListDto : BaseDto
    {
        public Employee Employee { get; set; }

        public string RequestingEmployeeId { get; set; }

        public LeaveTypeDto LeaveType { get; set; }

        public DateTime DateRequested { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool? Approve { get; set; }
    }
}
