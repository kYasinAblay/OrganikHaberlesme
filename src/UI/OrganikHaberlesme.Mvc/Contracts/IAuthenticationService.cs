using System.Threading.Tasks;

using OrganikHaberlesme.Mvc.Models.User;

namespace OrganikHaberlesme.Mvc.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string email, string password);

        Task<bool> Register(RegisterVm register);

        Task Logout();
    }
}
