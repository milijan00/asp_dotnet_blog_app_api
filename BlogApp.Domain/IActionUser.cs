using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain
{
    public interface IActionUser
    {
        string Identity { get;}
        public int Id { get;  }
        public IEnumerable<int> AllowedUseCasesIds { get; set; }
        public string Email { get; }
    }
}
