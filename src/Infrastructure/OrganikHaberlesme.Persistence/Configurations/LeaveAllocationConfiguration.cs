using OrganikHaberlesme.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrganikHaberlesme.Persistence.Configurations
{
    public class LeaveAllocationConfiguration : IEntityTypeConfiguration<LeaveAllocation>
    {
        public void Configure(EntityTypeBuilder<LeaveAllocation> builder)
        {
            //builder.HasData(
            //    new LeaveAllocation
            //    {
            //    },
            //    new LeaveAllocation
            //    {
            //    }
            // );
        }
    }
}
