using OrganikHaberlesme.Identity.Configurations;
using OrganikHaberlesme.Identity.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OrganikHaberlesme.Identity
{
    public class OrganikIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public OrganikIdentityDbContext(DbContextOptions<OrganikIdentityDbContext> options) 
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
