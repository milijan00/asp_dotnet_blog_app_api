using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using BlogApp.Implementations.Exceptions;
using BlogApp.Implementations.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfUpdateUserCommand : EfBase, IUpdateUserCommand
    {
        private UpdateUserValidator _validator;
        public EfUpdateUserCommand(BlogAppDbContext context,UpdateUserValidator validator) : base(context)
        {
            this._validator = validator;
        }

        public int Id => 32;

        public string Name => "EfUpdateUser";

        public string Description => "Update a user via entity framework.";

        public void Execute(UpdateUserDto request)
        {
            var result = this._validator.Validate(request);
            if (result.IsValid)
            {
                var user = this.context.Users.Find(request.Id.Value);
                if (!string.IsNullOrEmpty(request.Firstname))
                {
                    user.Firstname = request.Firstname;
                }

                if (!string.IsNullOrEmpty(request.Lastname))
                {
                    user.Lastname = request.Lastname;
                }

                if (!string.IsNullOrEmpty(request.Email))
                {
                    user.Email = request.Email;
                }

                if (!string.IsNullOrEmpty(request.Username))
                {
                    user.Username = request.Username;
                }

                if (!string.IsNullOrEmpty(request.Password))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                }
                if (!string.IsNullOrEmpty(request.ProfilePicture))
                {
                     
                    user.ProfilePicture = request.ProfilePicture; 
                }

                this.context.SaveChanges();
            }else
            {
                throw new UnproccessableEntityException(result.Errors);
            }
        }
    }
}
