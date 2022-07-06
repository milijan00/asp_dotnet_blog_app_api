using BlogApp.API.Extensions;
using BlogApp.Application.Dto;
using BlogApp.Application.Dto.searches;
using BlogApp.Application.UseCases.Commands;
using BlogApp.Application.UseCases.Queries;
using BlogApp.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LikedCommentsController : ControllerBase
    {
        private UseCaseHandler _handler;

        public LikedCommentsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] SearchForCommentsLikesDto dto, [FromServices] IGetCommentsLikesQuery getCommentsLikes)
        {
            return this.HandleUseCase(() =>
            {
                return Ok(this._handler.HandleQuery(getCommentsLikes, dto));
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] RequestLikedCommentDto dto, [FromServices] ICreateLikedCommentCommand createLikedComment)
        {
            return this.HandleUseCase(() =>
            {
                this._handler.HandleCommand(createLikedComment, dto);
                return StatusCode(201);
            });
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] RequestLikedCommentDto dto, [FromServices] IDeleteLikedCommentCommand deleteLikeComment)
        {
            return this.HandleUseCase(() =>
            {
                this._handler.HandleCommand(deleteLikeComment, dto);
                return NoContent();
            });
        }
    }
}
