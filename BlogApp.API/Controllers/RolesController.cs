using Microsoft.AspNetCore.Mvc;
using BlogApp.DataAccess;
using BlogApp.Application.Dto;
using System.Linq;
using BlogApp.Application.UseCases.Commands;
using BlogApp.Application.UseCases.Queries;
using BlogApp.Implementations;
using BlogApp.API.Extensions;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;

        public RolesController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }


        // GET: api/<RolesController>
        [HttpGet]
        public IActionResult Get([FromServices] IGetRolesQuery getRoles) 
        {
            //           try
            //           {
            //return Ok(this._useCaseHandler.HandleQuery(getRoles));
            //           }catch(Exception ex)
            //           {

            //           }
             return this.HandleUseCase(() =>
            {
                return Ok(this._useCaseHandler.HandleQuery(getRoles));
            });
            //return Ok();

        }

        // GET api/<RolesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindRoleQuery findRole) 
        {
            return this.HandleUseCase(() =>
            {
                return Ok(this._useCaseHandler.HandleQuery(findRole, id));
            });
        }

        // POST api/<RolesController>
        [HttpPost]
        public IActionResult Post([FromBody]RoleDto role, [FromServices] ICreateRoleCommand createRole)
        {
            this._useCaseHandler.HandleCommand(createRole, role);
            return StatusCode(201);
        }

        // PUT api/<RolesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RoleDto role, [FromServices] IUpdateRoleCommand updateRole)
        {
            return this.HandleUseCase(() =>
            {
                role.Id = id;
                this._useCaseHandler.HandleCommand(updateRole, role);
                return NoContent();
            });
        }

        // DELETE api/<RolesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteRoleCommand deleteRole)
        {
            return this.HandleUseCase(() =>
            {
                this._useCaseHandler.HandleCommand(deleteRole, id);
                return NoContent();
            });
        }
    }
}
