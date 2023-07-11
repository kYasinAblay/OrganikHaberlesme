using OrganikHaberlesme.Application.Contracts.Infrastructure;
using OrganikHaberlesme.Application.Models.Email;
using OrganikHaberlesme.Infrastructure.Mail;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OrganikHaberlesme.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
