using BlogApp.API.Extensions;
using BlogApp.Application.Dto;
using BlogApp.Application.Dto.searches;
using BlogApp.Application.UseCases.Commands;
using BlogApp.Application.UseCases.Queries;
using BlogApp.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;

        public CommentsController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }

        // GET: api/<Comments>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchCommentDto search, [FromServices] IGetCommentsQuery getComments)
        {
            return this.HandleUseCase(() =>
            {
                return Ok(this._useCaseHandler.HandleQuery(getComments, search));
            });
        }

        // GET api/<Comments>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindCommentQuery findComment)
        {
            return this.HandleUseCase(() =>
            {
                return Ok(this._useCaseHandler.HandleQuery(findComment, id));
            });
        }

       
        [HttpPost]
        public IActionResult Post([FromBody] CommentDto dto, [FromServices] ICreateCommentCommand createComment)
        {
            return this.HandleUseCase(() =>
            {
                this._useCaseHandler.HandleCommand(createComment, dto);
                return StatusCode(201);
            });
        }

        //PUT api/<Comments>/5
        [HttpPut]
        public IActionResult Put([FromBody] CommentDto dto, [FromServices] IUpdateCommentCommand command)
        {
            return this.HandleUseCase(() =>
            {
                this._useCaseHandler.HandleCommand(command, dto);
                return NoContent();
            });
        }

        // DELETE api/<Comments>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCommentCommand deleteComment)
        {
            return this.HandleUseCase(() =>
            {
                this._useCaseHandler.HandleCommand(deleteComment, id);
                return NoContent();
            });
        }
    }
}
