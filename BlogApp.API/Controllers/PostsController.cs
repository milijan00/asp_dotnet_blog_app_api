using BlogApp.API.Extensions;
using BlogApp.Application.Dto;
using BlogApp.Application.Dto.searches;
using BlogApp.Application.UseCases.Commands;
using BlogApp.Application.UseCases.Queries;
using BlogApp.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private UseCaseHandler _handler;

        public PostsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostDto dto, [FromServices] ICreatePostCommand createPost)
        {
            return this.HandleUseCase(() =>
            {
                this._handler.HandleCommand(createPost, dto);
                return StatusCode(201);
            });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromQuery] SearchPostDto dto, [FromServices] IGetPostsQuery getPosts)
        {
            return this.HandleUseCase(() =>
            {
                return Ok(this._handler.HandleQuery(getPosts, dto));
            });
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id, [FromServices]IFindPostQuery findPost)
        {
            return this.HandleUseCase(() =>
            {
                return Ok(this._handler.HandleQuery(findPost, id));
            });
        }
        [HttpPatch]
        public IActionResult Patch([FromBody] UpdatePostDto dto, [FromServices] IUpdatePostCommand command)
        {
            return this.HandleUseCase(() =>
            {
                this._handler.HandleCommand(command, dto);
                return NoContent();
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices]IDeletePostCommand deletePost )
        {
            return this.HandleUseCase(() =>
            {
                this._handler.HandleCommand(deletePost, id);
                return NoContent();
            });
        }
    }
}
