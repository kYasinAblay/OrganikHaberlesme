using System.Threading.Tasks;

using OrganikHaberlesme.Application.Contracts.Identity;
using OrganikHaberlesme.Application.Models.Identity;

using Microsoft.AspNetCore.Mvc;
using OrganikHaberlesme.Application.Responses;
using OrganikHaberlesme.Application.Models.VerificationCode;

namespace OrganikHaberlesme.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("otpLogin")]
        public async Task<ActionResult<AuthResponse>> OtpLogin(AuthOptions options)
        {
            return Ok(await _authService.OtpLogin(options));
        }
        [HttpPost("generateCode")]
        public async Task<ActionResult<VerificationNotify>> GenerateCode(string provider)
        {
            return Ok(await _authService.GenerateCode(provider));
        }
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            return Ok(await _authService.Login(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            return Ok(await _authService.Register(request));
        }
    }
}
