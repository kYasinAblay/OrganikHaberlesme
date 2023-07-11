using System.Threading.Tasks;

using OrganikHaberlesme.Application.Contracts.Identity;
using OrganikHaberlesme.Application.Models.Identity;

using Microsoft.AspNetCore.Mvc;

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
