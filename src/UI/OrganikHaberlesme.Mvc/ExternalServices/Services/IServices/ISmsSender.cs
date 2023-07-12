using System.Threading.Tasks;

using OrganikHaberlesme.Mvc.Services.Base;

namespace OrganikHaberlesme.Mvc.ExternalServices.Services.IServices
{
    public interface ISmsSender
    {
        Task<bool> SendSmsAsync(VerificationNotify verification);

    }
}
