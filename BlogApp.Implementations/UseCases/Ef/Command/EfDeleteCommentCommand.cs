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
    public class EfDeleteCommentCommand : EfBase, IDeleteCommentCommand
    {
        public EfDeleteCommentCommand(BlogAppDbContext context) : base(context)
        {
        }

        public int Id => 21;

        public string Name => "EfDeleteComment";

        public string Description => "Delete a comment via entity framework";

        public void Execute(int request)
        {
            var comment = this.context.Comments.Include(x => x.ChildComments).FirstOrDefault(x => x.Id == request && x.IsActive);
            if(comment == null)
            {
                throw new EntityNotFoundException();
            }
            this.context.DeactivateRange<Comment>(comment.ChildComments);
            comment.UsersWhoLiked.Clear();
            this.context.Deactivate<Comment>(request);
            this.context.SaveChanges();
        }
    }
}
