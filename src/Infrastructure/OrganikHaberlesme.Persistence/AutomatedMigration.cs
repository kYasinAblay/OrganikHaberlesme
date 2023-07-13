using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OrganikHaberlesme.Persistence
{
    public static class AutomatedMigration
    {
        public static async Task MigrateAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<ProgramDbContext>();

            if (context.Database.IsSqlServer() && context.Database.GetPendingMigrations().Count() > 0) await context.Database.MigrateAsync();

        }
    }
}
