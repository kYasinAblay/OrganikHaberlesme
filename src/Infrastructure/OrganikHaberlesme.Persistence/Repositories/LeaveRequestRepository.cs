using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OrganikHaberlesme.Application.Contracts.Persistence;
using OrganikHaberlesme.Domain;

using Microsoft.EntityFrameworkCore;

namespace OrganikHaberlesme.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly ProgramDbContext _dbcontext;

        public LeaveRequestRepository(ProgramDbContext dbContext)
            : base(dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId)
        {
            return await _dbcontext.LeaveRequests.Where(x => x.RequestingEmployeeId == userId).ToListAsync();
        }

        public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approvalStatus)
        {
            leaveRequest.Approved = approvalStatus;
            _dbcontext.Entry(leaveRequest).State = EntityState.Modified;
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            var leaveRequest = await _dbcontext.LeaveRequests
                .Include(x => x.LeaveType)
                .FirstAsync(x => x.Id == id);

            return leaveRequest;
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var leaveRequests = await _dbcontext.LeaveRequests
                .Include(x => x.LeaveType)
                .ToListAsync();

            return leaveRequests;
        }
    }
}
