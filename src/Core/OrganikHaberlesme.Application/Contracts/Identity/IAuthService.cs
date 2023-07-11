using System.Threading.Tasks;

using OrganikHaberlesme.Application.Models.Identity;

namespace OrganikHaberlesme.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);

        Task<RegistrationResponse> Register(RegistrationRequest request);
    }
}
