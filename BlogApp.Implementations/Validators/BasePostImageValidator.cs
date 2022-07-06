using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.Validators
{
    public class BasePostImageValidator<T> : AbstractValidator<T> where  T : class
    {
        protected List<string> Extensions { get; }
        public BasePostImageValidator()
        {
            this.Extensions = new List<string>() { ".jpg", ".png" };

            
        }
        protected string ExtensionsInOneRow {
            get
            {

                string extensionsInOneRow = "";
                this.Extensions.ForEach(e => extensionsInOneRow += e + ", ");
                return extensionsInOneRow;
            }
        }
        protected string ImageRegEx => @"^[A-z0-9\-\._]{1,50}\.(jpg|png|jpeg)$";

    }
}
