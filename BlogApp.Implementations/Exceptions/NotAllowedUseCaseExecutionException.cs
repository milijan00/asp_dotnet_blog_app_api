using BlogApp.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.Exceptions
{
    public class NotAllowedUseCaseExecutionException : Exception
    {
        public NotAllowedUseCaseExecutionException(string useCaseName, string identity)
        {
        
        }
    }
}
