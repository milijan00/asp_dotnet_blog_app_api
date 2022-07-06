using BlogApp.Application.Dto;
using BlogApp.Application.Dto.searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.UseCases.Queries
{
    public interface IGetPostsQuery : IQuery<SearchPostDto, SearchResult<SearchResultPostDto>>
    {
    }
}
