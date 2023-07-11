using System;
using System.ComponentModel.DataAnnotations;

using OrganikHaberlesme.Mvc.Models.Employee;
using OrganikHaberlesme.Mvc.Models.LeaveType;

namespace OrganikHaberlesme.Mvc.Models.LeaveRequest
{
    public class LeaveRequestVm : CreateLeaveRequestVm
    {
        public int Id { get; set; }

        [Display(Name = "Date Requested")]
        public DateTime DateRequested { get; set; }

        [Display(Name = "Date Actioned")]
        public DateTime DateActioned { get; set; }

        [Display(Name = "Approval State")]
        public bool? Approved { get; set; }

        public bool Cancelled { get; set; }

        public LeaveTypeVm LeaveType { get; set; }
        
        public EmployeeVm Employee { get; set; }
    }
}
