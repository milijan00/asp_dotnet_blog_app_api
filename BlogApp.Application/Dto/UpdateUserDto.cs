using Microsoft.AspNetCore.Http;

namespace BlogApp.Application.Dto
{
    public class UpdateUserDto : Dto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; }
    }
}
