using BlogApp.Domain;
using System.Collections.Generic;

namespace BlogApp.API.Core
{
    public class JwtUser : IActionUser
    {
        public string Identity { get; set; }

        public int Id { get; set; }
      
        public string Email { get; set; }

        public IEnumerable<int> AllowedUseCasesIds { get; set; } = new List<int> ();

         
    }
}
