using BlogApp.Implementations;
using BlogApp.Implementations.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogApp.API.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult HandleException(this ControllerBase c, Exception ex)
        {
            if(ex is NotAllowedUseCaseExecutionException)
            {
                return c.Forbid();
            }
            else if(ex is EntityNotFoundException)
            {
                return c.NotFound();
            }
            else if(ex is UnproccessableEntityException e)
            {
                return c.UnprocessableEntity(e.Errors);
            }
            else if(ex is UnauthorizedAccessException)
            {
                return c.Unauthorized();
            }
            else if(ex is InvalidOperationException ex1)
            {
                return c.BadRequest(new { error = ex1.Message });
            }
            return c.StatusCode(500);
        }
        public static IActionResult HandleUseCase(this ControllerBase c, Func<IActionResult > executeUseCase)
        {
            try
            {
                return executeUseCase();
            }catch(Exception ex)
            {
                return c.HandleException(ex);
            }
        }
    }
}
