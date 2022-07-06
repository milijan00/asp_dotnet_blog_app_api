using BlogApp.Application.Logging;
using BlogApp.Application.UseCases;
using BlogApp.Application.UseCases.Logging;
using BlogApp.Domain;
using BlogApp.Implementations.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations
{
    public class UseCaseHandler
    {
        private IExceptionLogger _exceptionLogger;
        private IUseCaseLogger _useCaseLogger;
        private IActionUser _actionUser;
        public UseCaseHandler(
            IExceptionLogger exceptionLogger,
            IUseCaseLogger useCaseLogger,
            IActionUser actionUser
            )
        {
            this._exceptionLogger = exceptionLogger;
            this._actionUser =  actionUser;
            this._useCaseLogger = useCaseLogger;
        }

        public void HandleCommand<TRequest>(ICommand<TRequest> command, TRequest data)
        {
            try
            {
                HandleLoggingAndAuthorization(command, data);
                command.Execute(data);
                
            }
            catch (Exception ex)
            {
                this._exceptionLogger.Log(ex);
                throw;
            }
        }
        public TResponse HandleQuery<TRequest, TResponse>(IQuery<TRequest, TResponse> query, TRequest data)
        {
            try
            {
                HandleLoggingAndAuthorization(query, data);
                var response = query.Execute(data);
                return response;
            }
            catch (Exception ex)
            {
                this._exceptionLogger.Log(ex);
                throw;
            }
        }
        public TResponse HandleQuery< TResponse>(IQuery< TResponse> query)
        {
            try
            {
                // ovde dolazi do greske jer ne postoji data
                HandleLoggingAndAuthorization(query);
                var response = query.Execute();
                return response;
            }
            catch (Exception ex)
            {
                this._exceptionLogger.Log(ex);
                throw;
            }
        }

        private void HandleLoggingAndAuthorization<TRequest>(IUseCase useCase, TRequest data )
        {
            var isAuthorized = this._actionUser.AllowedUseCasesIds.Contains(useCase.Id);
            var log = new UseCaseLog
            {
                User = this._actionUser.Identity,
                ExecutionDateTime = DateTime.UtcNow,
                UseCaseName = useCase.Name,
                UserId = this._actionUser.Id,
                Data =  JsonConvert.SerializeObject(data),
                IsAuthorized = isAuthorized
            };
            this._useCaseLogger.Log(log);
            if (!isAuthorized)
            {
                throw new NotAllowedUseCaseExecutionException(useCase.Name, this._actionUser.Identity);
            }
        }
     private void HandleLoggingAndAuthorization(IUseCase useCase)
        {
            var isAuthorized = this._actionUser.AllowedUseCasesIds.Contains(useCase.Id);
            var log = new UseCaseLog
            {
                User = this._actionUser.Identity,
                ExecutionDateTime = DateTime.UtcNow,
                UseCaseName = useCase.Name,
                UserId = this._actionUser.Id,
                IsAuthorized = isAuthorized
            };
            this._useCaseLogger.Log(log);
            if (!isAuthorized)
            {
                throw new NotAllowedUseCaseExecutionException(useCase.Name, this._actionUser.Identity);
            }
        }

        
    }
}
