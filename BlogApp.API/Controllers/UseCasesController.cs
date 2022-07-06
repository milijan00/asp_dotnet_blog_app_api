using BlogApp.API.Extensions;
using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UseCasesController : ControllerBase
    {
        private UseCaseHandler _handler;

        public UseCasesController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpPatch] 
       public IActionResult Patch([FromBody] UpdateUseCasesDto dto, [FromServices] IUpdateUseCasesCommand command)
        {
            return this.HandleUseCase(() =>
            {
                this._handler.HandleCommand(command, dto);
                return NoContent();
            });
        }
    }
}
