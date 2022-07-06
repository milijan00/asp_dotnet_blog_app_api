using BlogApp.Application.Dto;
using BlogApp.Application.UseCases.Commands;
using BlogApp.DataAccess;
using BlogApp.Domain;
using BlogApp.Implementations.Exceptions;
using BlogApp.Implementations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.UseCases.Ef.Command
{
    public class EfUpdateUseCasesCommand : EfBase, IUpdateUseCasesCommand
    {
        private UpdateUseCasesValidator _validator;
        public EfUpdateUseCasesCommand(BlogAppDbContext context, UpdateUseCasesValidator validator) : base(context)
        {
            this._validator = validator;
        }

        public int Id => 31;

        public string Name => "EfUpdateUseCases";

        public string Description => "Update use cases via entity framework.";

        public void Execute(UpdateUseCasesDto request)
        {
            var result = this._validator.Validate(request);
            if (result.IsValid)
            {
                var notInsertedUseCasesIds = request.UseCasesIds.Where(x => !this.context.UsersUseCases.Where(u => u.UserId == request.UserId).Any(u => u.UseCaseId == x)).Select(x => new UserUseCase 
                {
                    UserId = request.UserId,
                    UseCaseId = x
                }).ToList();

                this.context.UsersUseCases.AddRange(notInsertedUseCasesIds);
                this.context.SaveChanges();
            }
            else
            {
                throw new UnproccessableEntityException(result.Errors);
            }
        }
    }
}
