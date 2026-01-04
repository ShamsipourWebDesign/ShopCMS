using Microsoft.AspNetCore.Mvc;
using ShopCMS.Infrastructure.Auth.Interfaces;
using ShopCMS.Infrastructure.Auth.Dtos;

namespace ShopCMS.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest req)
            => Ok(await _auth.LoginAsync(req));

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshRequest req)
            => Ok(await _auth.RefreshAsync(req));
    }

}
