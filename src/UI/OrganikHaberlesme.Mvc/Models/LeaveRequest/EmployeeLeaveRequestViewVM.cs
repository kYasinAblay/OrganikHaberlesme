using System.Collections.Generic;

using OrganikHaberlesme.Mvc.Models.LeaveAllocation;

namespace OrganikHaberlesme.Mvc.Models.LeaveRequest
{
    public class EmployeeLeaveRequestViewVm
    {
        public List<LeaveAllocationVm> LeaveAllocations { get; set; }

        public List<LeaveRequestVm> LeaveRequests { get; set; }
    }
}
