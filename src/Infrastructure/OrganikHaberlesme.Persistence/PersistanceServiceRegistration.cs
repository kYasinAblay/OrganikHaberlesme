using OrganikHaberlesme.Application.Contracts.Persistence;
using OrganikHaberlesme.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OrganikHaberlesme.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection ConfigurePersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<ProgramDbContext>(options =>
                {
                    options.UseSqlServer(
                        configuration.GetConnectionString("OrganikHaberlesmeConnectionString"),
                        b => b.MigrationsAssembly(typeof(ProgramDbContext).Assembly.FullName));
                });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();

            return services;
        }
    }
}
