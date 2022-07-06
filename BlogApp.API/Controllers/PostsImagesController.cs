using BlogApp.API.Extensions;
using BlogApp.Application.Dto;
using BlogApp.Application.Dto.searches;
using BlogApp.Application.UseCases.Commands;
using BlogApp.Application.UseCases.Queries;
using BlogApp.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsImagesController : ControllerBase
    {
        private UseCaseHandler _handler;

        public PostsImagesController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] SearchPostImageDto dto, [FromServices] IGetPostImagesQuery getPostImages)
        {
            return this.HandleUseCase(() =>
            {
                return Ok(this._handler.HandleQuery(getPostImages, dto));
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreatePostImageDto dto, [FromServices] ICreatePostImageCommand command)
        {
            return this.HandleUseCase(() =>
            {
                this._handler.HandleCommand(command, dto);
                return StatusCode(201);
            });
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] DeletePostsImagesDto dto, [FromServices] IDeletePostsImagesCommand command)
        {
            return this.HandleUseCase(() =>
            {
                this._handler.HandleCommand(command, dto);
                return NoContent();
            });
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpdatePostImagesDto dto, [FromServices] IUpdatePostImagesCommand command)
        {
            return this.HandleUseCase(() =>
            {
                this._handler.HandleCommand(command, dto);
                return NoContent();
            });
        }
    }
}
