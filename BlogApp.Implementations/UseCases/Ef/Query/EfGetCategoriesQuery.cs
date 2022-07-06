using AutoMapper;
using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Query
{
    public class EfGetCategoriesQuery : EfBase,  IGetCategoriesQuery
    {
        private IMapper _mapper;
        public EfGetCategoriesQuery(BlogApp.DataAccess.BlogAppDbContext context, IMapper mapper) : base(context) { _mapper = mapper; }
        public string Name => "Get all categories.";

        public string Description => "Get all categories.";

        public int Id => 10;

        public IEnumerable<CategoryDto> Execute()
        {
            var categories =  this.context.Categories.Where(c => c.IsActive).ToList();
            return this._mapper.Map< List<CategoryDto>>(categories);
        }
    }
}
