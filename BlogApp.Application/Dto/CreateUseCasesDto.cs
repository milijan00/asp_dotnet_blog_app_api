using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Dto
{
    public class CreateUseCasesDto  
    {
        public IEnumerable<UseCaseDto> UseCases { get; set; }
    }
}
