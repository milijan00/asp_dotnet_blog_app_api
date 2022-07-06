using BlogApp.Application.Dto;
using Microsoft.AspNetCore.Http;

namespace BlogApp.API.Core.Dto
{
    public class UpdateUserDtoWithImage : UpdateUserDto
    {
        public IFormFile Image { get; set; }
    }
}
