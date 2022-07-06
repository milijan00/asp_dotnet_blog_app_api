using BlogApp.Application.Dto;
using BlogApp.Application.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Implementations.Emails
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly string _emailFrom; 
        private readonly string _password; 
        public SmtpEmailSender(string EmailFrom, string Password)
        {
            this._emailFrom = EmailFrom;
            this._password = Password;
        }
        public void Send(EmailDto dto)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_emailFrom, _password),
                UseDefaultCredentials = false
            };
            var mailMessage = new MailMessage(this._emailFrom, dto.To);
            mailMessage.Subject = dto.Subject;
            mailMessage.Body = dto.Body;
            mailMessage.IsBodyHtml = true;

            smtp.Send(mailMessage);

        }
    }
}
