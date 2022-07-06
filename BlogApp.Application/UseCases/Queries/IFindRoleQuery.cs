using BlogApp.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.UseCases.Queries
{
    public interface IFindRoleQuery : IQuery<int, RoleDto> 
    {
        RoleDto Execute(int id);
    }
}
