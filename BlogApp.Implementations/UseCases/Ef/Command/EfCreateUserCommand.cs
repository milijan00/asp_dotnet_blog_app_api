using BlogApp.Application.Dto;
using BlogApp.Application.Emails;
using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using BlogApp.Domain;
using BlogApp.Implementations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfCreateUserCommand : EfBase, ICreateUserCommand
    {
        private UserValidator _validator;
        private IEmailSender _email;
        public EfCreateUserCommand(BlogAppDbContext context, UserValidator validator, IEmailSender email) : base(context)
        {
            this._validator = validator;
            _email = email;
        }

        public string Name => "EfCreateUserCommand";

        public string Description => "Create a user via entity framework";

        public int Id => 2;

        public void Execute(RegisteredUserDto request)
        {
            var result = this._validator.Validate(request);
            var userRole = 4;
            if (result.IsValid)
            {
                var password = BCrypt.Net.BCrypt.HashPassword(request.Password);
                request.ImageSource =  "defaultUserImage.jpg";
                var user = new User
                {
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    Email = request.Email,
                    Username = request.Username,
                    RoleId = userRole,
                    Password = password,
                    ProfilePicture = request.ImageSource
                };

                this.context.Users.Add(user);
                //this.context.SaveChanges();

                this._email.Send(new EmailDto
                {
                    To = request.Email,
                    Body = "You have successfuly registered!",
                    Subject = "Registration status"
                });
            }
            else
            {
                result.Errors.ForEach(x => Console.WriteLine($"{x.PropertyName} : {x.ErrorMessage}"));
            }
        }
    }
}
