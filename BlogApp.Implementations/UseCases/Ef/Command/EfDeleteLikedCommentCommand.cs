using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using BlogApp.Implementations.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfDeleteLikedCommentCommand : EfBase, IDeleteLikedCommentCommand
    {
        public EfDeleteLikedCommentCommand(BlogAppDbContext context) : base(context)
        {
        }

        public int Id => 24; 

        public string Name => "EfDeleteLikedComment";

        public string Description => "Delete a like via entity framework.";

        public void Execute(RequestLikedCommentDto request)
        {
            var likedComment = this.context.LikedComments.FirstOrDefault(lc => lc.CommentId == request.CommentId && lc.UserId == request.UserId);
            if(likedComment  == null)
            {
                throw new EntityNotFoundException("Liked comment", request.CommentId);
            }
            this.context.LikedComments.Remove(likedComment);
            this.context.SaveChanges();
        }
    }
}
