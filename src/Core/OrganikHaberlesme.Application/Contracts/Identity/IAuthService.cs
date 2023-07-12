using System.Threading.Tasks;

using OrganikHaberlesme.Application.Models.Identity;
using OrganikHaberlesme.Application.Models.VerificationCode;
using OrganikHaberlesme.Application.Responses;

namespace OrganikHaberlesme.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<VerificationNotify> GenerateCode(string provider);
        Task<AuthResponse> OtpLogin(AuthOptions options);
        Task<AuthResponse> Login(AuthRequest request);
        Task<RegistrationResponse> Register(RegistrationRequest request);
    }
}
