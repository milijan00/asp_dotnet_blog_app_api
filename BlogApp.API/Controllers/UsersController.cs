using BlogApp.API.Core;
using BlogApp.API.Core.Dto;
using BlogApp.API.Extensions;
using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.Application.UseCases.Queries;
using BlogApp.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private UseCaseHandler _handler;
        private List<string> _allowedExtensions = new List<string>() { ".jpg", ".png", ".jpeg" };
        public UsersController(UseCaseHandler useCaseHandler)
        {
            _handler = useCaseHandler;
            
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get([FromServices] IGetUsersQuery getUsers)
        {
            return this.HandleUseCase(() =>
            {
                return Ok(this._handler.HandleQuery(getUsers));
            });
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id, [FromServices] IFindUserQuery query)
        {
            return this.HandleUseCase(() =>
            {
                return Ok(this._handler.HandleQuery(query, id));
            });
        }

        // POST api/<UsersController>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] RegisteredUserDto dto, [FromServices] ICreateUserCommand createUser)
        {
            return this.HandleUseCase(() =>
            {
                this._handler.HandleCommand(createUser, dto);
                return StatusCode(201);
            });
        }

        [HttpPatch]
        public IActionResult Patch([FromForm] UpdateUserDtoWithImage dto, [FromServices] IUpdateUserCommand command)
        {
            return this.HandleUseCase(() =>
            {
                if(dto.Image != null)
                {
                    var extension = Path.GetExtension(dto.Image.FileName);
                    if (!this._allowedExtensions.Contains(extension))
                    {
                        throw new InvalidOperationException("File extension is unacceptable.");
                    }
                    var fileName = Guid.NewGuid().ToString() + extension;
                    var filePath = Path.Combine("wwwroot", "images", fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    dto.Image.CopyTo(stream);
                    dto.ProfilePicture = fileName;
                }

                this._handler.HandleCommand(command, dto);
                return NoContent();
            });
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteUserCommand deleteUser )
        {
            return this.HandleUseCase(() =>
            {
                this._handler.HandleCommand(deleteUser, id);
                return NoContent();
            });
        }
    }

}
