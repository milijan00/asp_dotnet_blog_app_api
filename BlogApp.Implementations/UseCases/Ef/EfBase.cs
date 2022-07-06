using BlogApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef
{
    public class EfBase 
    {
        protected BlogAppDbContext context;
        public EfBase(BlogAppDbContext context)
        {
            this.context = context;
        }
    }
}
