using OrganikHaberlesme.Application.Contracts.Persistence;
using OrganikHaberlesme.Domain;

namespace OrganikHaberlesme.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        private readonly ProgramDbContext _dbcontext;

        public LeaveTypeRepository(ProgramDbContext dbContext)
            : base(dbContext)
        {
            _dbcontext = dbContext;
        }
    }
}
