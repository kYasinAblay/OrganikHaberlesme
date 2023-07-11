using System.Collections.Generic;
using System.Threading.Tasks;

using OrganikHaberlesme.Domain;

namespace OrganikHaberlesme.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<LeaveRequest> GetLeaveRequestWithDetails(int id);

        Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();

        Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId);

        Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approvalStatus);
    }
}
