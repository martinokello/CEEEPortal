using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using CEEEServices.Interfaces;

namespace CEEEServices
{
    public class EmailService: IMailService
    {
        private readonly SmtpClient smtpClient;

        public EmailService()
        {
            var smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            smtpClient = new SmtpClient(smtpServer);
        }

        public EmailService(string smtpHost)
        {
            smtpClient = new SmtpClient(smtpHost);
        }

        public bool SendMail(MailMessage message)
        {
            try
            {
                smtpClient.Send(message);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
