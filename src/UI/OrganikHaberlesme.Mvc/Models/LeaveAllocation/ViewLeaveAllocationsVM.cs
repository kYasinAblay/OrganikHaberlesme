using System.Collections.Generic;

namespace OrganikHaberlesme.Mvc.Models.LeaveAllocation
{
    public class ViewLeaveAllocationsVm
    {
        public string EmployeeId { get; set; }

        public List<LeaveAllocationVm> LeaveAllocations { get; set; }
    }
}
