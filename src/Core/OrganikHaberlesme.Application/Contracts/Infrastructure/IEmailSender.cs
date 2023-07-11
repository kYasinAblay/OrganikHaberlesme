using System.Threading.Tasks;

using OrganikHaberlesme.Application.Models.Email;

namespace OrganikHaberlesme.Application.Contracts.Infrastructure
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Email email);
    }
}
