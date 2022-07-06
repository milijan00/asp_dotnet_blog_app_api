using BlogApp.API.Extensions;
using BlogApp.Application.Dto;
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
    public class CategoriesController : ControllerBase
    {
        private UseCaseHandler _useCaseHandler;

        public CategoriesController(UseCaseHandler useCaseHandler)
        {
            _useCaseHandler = useCaseHandler;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public IActionResult Get([FromServices] IGetCategoriesQuery getCategories)
        {
            return this.HandleUseCase(() =>
            {
                return Ok(this._useCaseHandler.HandleQuery(getCategories));
            });
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IFindCategoryQuery findCategory)
        {
            return this.HandleUseCase(() =>
            {
                return Ok(this._useCaseHandler.HandleQuery(findCategory, id));
            });
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public IActionResult Post([FromBody] CategoryDto dto, [FromServices] ICreateCategoryCommand createCategory)
        {
            return this.HandleUseCase(() =>
            {
                this._useCaseHandler.HandleCommand(createCategory, dto);
                return StatusCode(201);
            });
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryDto dto, [FromServices] IUpdateCategoryCommand updateCategory) 
        {
            return this.HandleUseCase(() =>
            {
                dto.Id = id;
                this._useCaseHandler.HandleCommand(updateCategory, dto);
                return NoContent();
            });
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCategoryCommand deleteCategory)
        {
            return this.HandleUseCase(() =>
            {
                this._useCaseHandler.HandleCommand(deleteCategory, id);
                return NoContent();
            });
        }
    }
}
