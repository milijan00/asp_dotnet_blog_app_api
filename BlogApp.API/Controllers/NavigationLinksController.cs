using BlogApp.API.Extensions;
using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.Application.UseCases.Queries;
using BlogApp.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavigationLinksController : ControllerBase
    {
        private UseCaseHandler _handler;

        public NavigationLinksController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromServices] IGetNavigationLinksQuery query)
        {
            return this.HandleUseCase(() =>
            {
                return Ok(this._handler.HandleQuery(query));
            });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindNavigationLinkQuery query)
        {
            return this.HandleUseCase(() =>
            {
                return Ok(this._handler.HandleQuery(query, id));
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] NavigationLInkDto dto,[FromServices] ICreateNavigationLinkCommand command)
        {
            return this.HandleUseCase(() =>
            {
                this._handler.HandleCommand(command, dto);
                return StatusCode(201);
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteNavigationLinkCommand command)
        {
            return this.HandleUseCase(() =>
            {
                this._handler.HandleCommand(command, id);
                return NoContent();
            });
        }

        [HttpPut]
        public IActionResult Put([FromBody] NavigationLInkDto dto, [FromServices] IUpdateNavigationLinkCommand command)
        {
            return this.HandleUseCase(() =>
            {
                this._handler.HandleCommand(command, dto);
                return NoContent();
            });
        }
    }
}
