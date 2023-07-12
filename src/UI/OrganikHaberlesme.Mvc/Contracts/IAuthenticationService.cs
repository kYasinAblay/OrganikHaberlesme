using System.Threading.Tasks;

using OrganikHaberlesme.Mvc.Models.User;
using OrganikHaberlesme.Mvc.Services.Base;

namespace OrganikHaberlesme.Mvc.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool?> AuthenticateOtp(AuthOptions options);
        Task<VerificationNotify> GetLogin2FACode(string provider);
        Task<bool?> Authenticate(string email, string password);

        Task<bool> Register(RegisterVm register);

        Task Logout();
    }
}
