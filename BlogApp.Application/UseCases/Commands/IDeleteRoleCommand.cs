using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.UseCases.Commands
{
    public interface IDeleteRoleCommand : ICommand<int>
    {
        void Execute(int id);
    }
}
