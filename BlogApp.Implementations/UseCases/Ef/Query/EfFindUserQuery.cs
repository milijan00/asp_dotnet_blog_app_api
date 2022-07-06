using AutoMapper;
using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Queries;
using BlogApp.DataAccess;
using BlogApp.Domain;
using BlogApp.Implementations.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Query
{
    public class EfFindUserQuery : EfBase, IFindUserQuery
    {
        private IMapper _mapper;
        public EfFindUserQuery(BlogAppDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 16;

        public string Name => "EfFindUser";

        public string Description => "Find a user via entity framework";

        public UserDto Execute(int request)
        {
            var user = this.context.Users.Find(request);
            if(user == null)
            {
                throw new EntityNotFoundException(nameof(User), request);
            }
            return this._mapper.Map<UserDto>(user);

        }
    }
}
