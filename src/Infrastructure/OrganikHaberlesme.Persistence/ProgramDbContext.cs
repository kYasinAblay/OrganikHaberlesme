using OrganikHaberlesme.Domain;

using Microsoft.EntityFrameworkCore;

namespace OrganikHaberlesme.Persistence
{
    public class ProgramDbContext : AuditableDbContext
    {
        public ProgramDbContext(DbContextOptions<ProgramDbContext> options)
            : base(options)
        {
        }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        public DbSet<LeaveType> LeaveTypes { get; set; }

        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProgramDbContext).Assembly);
        }
    }
}
