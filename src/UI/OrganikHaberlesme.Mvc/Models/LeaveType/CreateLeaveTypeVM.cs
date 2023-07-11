using System.ComponentModel.DataAnnotations;

namespace OrganikHaberlesme.Mvc.Models.LeaveType
{
    public class CreateLeaveTypeVm
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Default Number Of Days")]
        public int DefaultDays { get; set; }
    }
}

