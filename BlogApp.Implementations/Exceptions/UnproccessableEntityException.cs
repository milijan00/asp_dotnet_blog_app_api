using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.Exceptions
{
    public class UnproccessableEntityException : Exception
    {
        public UnproccessableEntityException(IEnumerable<ValidationFailure> errors)
        {
            Errors = errors;
        }

        public IEnumerable<ValidationFailure> Errors { get; }
    }
}
