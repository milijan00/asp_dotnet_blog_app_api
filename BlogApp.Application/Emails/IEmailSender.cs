using BlogApp.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Emails
{
    public interface IEmailSender
    {
        void Send(EmailDto dto);
    }
}
