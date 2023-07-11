using System.Threading.Tasks;

using OrganikHaberlesme.Mvc.Models.LeaveRequest;
using OrganikHaberlesme.Mvc.Services.Base;

namespace OrganikHaberlesme.Mvc.Contracts
{
    public interface ILeaveRequestService
    {
        Task<AdminLeaveRequestViewVm> GetAdminLeaveRequestList();

        Task<EmployeeLeaveRequestViewVm> GetUserLeaveRequests();

        Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestVm leaveRequest);

        Task<LeaveRequestVm> GetLeaveRequest(int id);

        Task DeleteLeaveRequest(int id);

        Task ApproveLeaveRequest(int id, bool approved);
    }
}
