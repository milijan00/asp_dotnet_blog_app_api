using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain
{
    public class NavigationLink : Entity
    {
        public string Name { get; set; }
        public string Route { get; set; }
    }
}
