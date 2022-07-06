using BlogApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Dto.searches
{
    public class SearchPostDto  : BasePagedSearch
    {
        public string Keyword { get; set; }
    }

    public class SearchResultPostDto : Dto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Author { get; set; }
        public string AuthorImage { get; set; }
        public ICollection<PostImageDto> Images { get; set; } = new List<PostImageDto>();
        public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
    }
}
