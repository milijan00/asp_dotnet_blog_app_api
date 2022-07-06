using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using BlogApp.DataAccess.Exceptions;
using BlogApp.DataAccess.Extensions;
using BlogApp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfDeletePostCommand : EfBase, IDeletePostCommand
    {
        public EfDeletePostCommand(BlogAppDbContext context) : base(context)
        {
        }

        public int Id => 21;

        public string Name => "EfDeletePost";

        public string Description => "Delete a post via entity framework.";

        public void Execute(int id)
        {
            var post = this.context.Posts.Include(p => p.PostsImages).Include(p => p.UsersWhoLiked).Include(p => p.Comments).FirstOrDefault(p => p.Id == id);
            if(post == null)
            {
                throw new EntityNotFoundException();
            }
            this.context.DeactivateRange(post.PostsImages);
            this.context.DeactivateRange(post.Comments);
            post.UsersWhoLiked.Clear();
            this.context.Deactivate<Post>(post.Id);
            this.context.SaveChanges();
        }
    }
}
