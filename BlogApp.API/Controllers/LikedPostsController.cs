using BlogApp.API.Extensions;
using BlogApp.Application.Dto;
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
    public class LikedPostsController : ControllerBase
    {
        private UseCaseHandler _handler;

        public LikedPostsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetPostsLikesQuery query)
        {
            return this.HandleUseCase(() =>
            {
                return Ok(this._handler.HandleQuery(query, id));
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] RequestPostsLikeDto dto, [FromServices] ICreatePostsLikeCommand command)
        {
            return this.HandleUseCase(() =>
            {
                this._handler.HandleCommand(command, dto);
                return StatusCode(201);
            });
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] RequestPostsLikeDto dto,[FromServices] IDeletePostsLikeCommand command)
        {
            return this.HandleUseCase(() =>
            {
                this._handler.HandleCommand(command, dto);
                return StatusCode(204);
            });
        }
    }
}
