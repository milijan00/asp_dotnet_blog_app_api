using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApp.Application.Dto;
namespace BlogApp.Application.UseCases.Queries
{
    public interface IGetRolesQuery : IQuery< IEnumerable<RoleDto>>
    {
    }
}
