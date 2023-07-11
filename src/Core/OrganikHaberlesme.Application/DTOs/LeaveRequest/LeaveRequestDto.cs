using System;

using OrganikHaberlesme.Application.DTOs.Common;
using OrganikHaberlesme.Application.DTOs.LeaveType;
using OrganikHaberlesme.Application.Models.Identity;

namespace OrganikHaberlesme.Application.DTOs.LeaveRequest
{
    public class LeaveRequestDto : BaseDto, ILeaveRequestDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Employee Employee { get; set; }

        public string RequestingEmployeeId { get; set; }

        public LeaveTypeDto LeaveType { get; set; }

        public int LeaveTypeId { get; set; }

        public DateTime DateRequested { get; set; }

        public string RequestComments { get; set; }

        public DateTime? DateActioned { get; set; }

        public bool? Approved { get; set; }

        public bool Cancelled { get; set; }
    }
}
