using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Queries;
using BlogApp.Domain;
using BlogApp.Implementations.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Query
{
    public class EfFindRoleQuery : EfBase, IFindRoleQuery
    {
        private AutoMapper.IMapper _mapper;
        public EfFindRoleQuery(BlogApp.DataAccess.BlogAppDbContext context,AutoMapper.IMapper mapper ) : base(context) { _mapper = mapper; }
        public string Name => "Find a specific role";

        public string Description => "Find a specific role via id.";

        public int Id => 9;

        public RoleDto Execute(int id)
        {
            var role =  this.context.Roles.FirstOrDefault(x => x.Id == id && x.IsActive);
            if(role == null)
            {
                throw new EntityNotFoundException(nameof(Role), id);
            }
            return this._mapper.Map<RoleDto>(role);
        }
    }
}
