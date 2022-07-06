using BlogApp.API.Core;
using BlogApp.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private JwtManager _jwtManager;

        public TokensController(JwtManager jwtManager)
        {
            _jwtManager = jwtManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] TokenRequest tokenRequest)
        {
            return this.HandleUseCase(() =>
            {
                var token = this._jwtManager.MakeToken(tokenRequest.Email, tokenRequest.Password);
                return Ok(new {token});
            });
        }
    }
}
