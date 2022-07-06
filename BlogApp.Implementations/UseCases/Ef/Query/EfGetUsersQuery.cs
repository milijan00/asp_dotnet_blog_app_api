using AutoMapper;
using BlogApp.Application.Dto;
using BlogApp.Application.UseCases;
using BlogApp.Application.UseCases.Queries;
using BlogApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Query
{
    public class EfGetUsersQuery : EfBase, IGetUsersQuery
    {
        private IMapper _mapper;
        public EfGetUsersQuery(BlogAppDbContext context,IMapper mapper ) : base(context)
        {
            _mapper = mapper;
        }

        public string Name => "EfGetUsers";

        public string Description => "Get all users via entity framework";

        public int Id => 13;

        IEnumerable<UserDto> IQuery<IEnumerable<UserDto>>.Execute()
        {
            var users = this.context.Users.Where(u => u.IsActive).ToList();

            return this._mapper.Map<List<UserDto>>(users);
        }
    }
}
