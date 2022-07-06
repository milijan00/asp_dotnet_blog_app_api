using BlogApp.Domain;
using System.Collections.Generic;

namespace BlogApp.API.Core
{

    public class ActionUser : IActionUser
    {
        public string Identity => "Anonmymous";

        public IEnumerable<int> AllowedUseCasesIds { get; set; } = new List<int>()
        {
            16, 20, 11, 2
        }; // dodati usecase id za reg i logovanje

        public string Email => "anonymous@gmail.com";

        int IActionUser.Id => 0;
    }
}
