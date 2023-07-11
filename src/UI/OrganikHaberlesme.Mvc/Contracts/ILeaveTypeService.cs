using System.Collections.Generic;
using System.Threading.Tasks;

using OrganikHaberlesme.Mvc.Models.LeaveType;
using OrganikHaberlesme.Mvc.Services.Base;

namespace OrganikHaberlesme.Mvc.Contracts
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeVm>> GetLeaveTypes();

        Task<LeaveTypeVm> GetLeaveTypeDetails(int id);

        Task<Response<int>> CreateLeaveType(CreateLeaveTypeVm createLeaveType);

        Task<Response<int>> UpdateLeaveType(int id, LeaveTypeVm leaveType);

        Task<Response<int>> DeleteLeaveType(int id);
    }
}
