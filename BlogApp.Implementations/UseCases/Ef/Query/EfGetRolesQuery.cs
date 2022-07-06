using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Query
{
    public class EfGetRolesQuery : EfBase, IGetRolesQuery
    {
        private AutoMapper.IMapper _mapper;
        public EfGetRolesQuery(BlogApp.DataAccess.BlogAppDbContext context, AutoMapper.IMapper mapper) : base(context) { _mapper = mapper; }
        public string Name => "Get all roles";
        public string Description => "Get all roles from the database";

        public int Id => 12;

        public IEnumerable<RoleDto> Execute()
        {
            var roles = this.context.Roles.Where(r => r.IsActive).ToList();
            return this._mapper.Map<List<RoleDto>>(roles);
        }
    }
}
