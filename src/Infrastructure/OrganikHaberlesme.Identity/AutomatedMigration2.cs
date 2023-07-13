using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OrganikHaberlesme.Identity
{

    public static class AutomatedMigration2
    {
        public static async Task MigrateAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<OrganikIdentityDbContext>();

            if (context.Database.IsSqlServer() && context.Database.GetPendingMigrations().Count() > 0) await context.Database.MigrateAsync();


            var result = context.Database.ExecuteSqlRaw(@"IF NOT
            EXISTS(SELECT * FROM sys.databases WHERE name = 'OrganikHaberlesmeHangFire')  BEGIN
            CREATE DATABASE OrganikHaberlesmeHangFire
             END");
        }
    }

}
