using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogApp.Domain;
using BlogApp.DataAccess.Exceptions;
namespace BlogApp.DataAccess.Extensions
{
    public static  class DbContextExtensions
    {
        public static void Deactivate(this DbContext context, Entity entity)
        {
            entity.IsActive = false;
            context.Entry(entity).State = EntityState.Modified;
        }
        public static void Deactivate<T>(this DbContext context, int id)
            where T : Entity
        {
            var itemToDeactivate = context.Set<T>().Find(id);
            
            if (itemToDeactivate == null)
            {
                throw new EntityNotFoundException();
            }

            itemToDeactivate.IsActive = false;
            context.Entry(itemToDeactivate).State = EntityState.Modified;

        }
        public static void Deactivate<T>(this DbContext context, IEnumerable<int> ids)
            where T : Entity
        {
           var toDeactivate = context.Set<T>().Where(x => ids.Contains(x.Id));

            foreach(var d in toDeactivate)
            {
                d.IsActive = false;
                //context.Entry(d).State = EntityState.Modified;
            }
        }
        public static void DeactivateRange<T>(this DbContext context, ICollection<T> elements)
            where T : Entity
        {
            foreach(var el in elements)
            {
                el.IsActive = false;
                //context.Entry(el).State = EntityState.Modified;
            }
        }
    }
}
