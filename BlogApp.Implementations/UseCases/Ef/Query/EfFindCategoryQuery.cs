using AutoMapper;
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
    public class EfFindCategoryQuery : EfBase, IFindCategoryQuery
    {
        private IMapper _mapper;
        public EfFindCategoryQuery(BlogApp.DataAccess.BlogAppDbContext context, IMapper mapper) : base(context) { _mapper = mapper; }
        public string Name => "EfFindCategory";

        public string Description => "Find a specific category via the id";

        public int Id => 8;

        public CategoryDto Execute(int request)
        {
            var category = this.context.Categories.FirstOrDefault(x => x.Id == request && x.IsActive);
            if(category == null)
            {
                throw new EntityNotFoundException(nameof(Category), request);
            }
            return this._mapper.Map<Category, CategoryDto>(category);
        }
    }
}
