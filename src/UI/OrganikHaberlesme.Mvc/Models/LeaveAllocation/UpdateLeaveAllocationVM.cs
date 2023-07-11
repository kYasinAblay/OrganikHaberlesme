using System.ComponentModel.DataAnnotations;

using OrganikHaberlesme.Mvc.Models.LeaveType;

namespace OrganikHaberlesme.Mvc.Models.LeaveAllocation
{
    public class UpdateLeaveAllocationVm
    {
        public int Id { get; set; }

        [Display(Name = "Number Of Days")]
        [Range(1, 50, ErrorMessage = "Enter Valid Number")]

        public int NumberOfDays { get; set; }
        
        public LeaveTypeVm LeaveType { get; set; }
    }
}
