using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;

namespace EmailServiceWorkFLow
{
    public static class EmailService
    {
        private static readonly string _user;

        private static readonly string _password;

        private static readonly string _userEmail;

        private static readonly string _smtpHost;

        private static readonly int _smtpPort;

        static EmailService()
        {
            _user = "lv166net@yahoo.com";
            _password = "121212-00";
            _userEmail = "lv166net@yahoo.com";
            _smtpHost = "smtp.mail.yahoo.com";
            _smtpPort = 587;
        }

        private static async Task<bool> SendEmail(EmailTemplateModel emailTemplate)
        {
            try
            {
                var client = new SmtpClient(_smtpHost, _smtpPort)
                {
                    Credentials = new NetworkCredential(_user, _password),
                    EnableSsl = true
                };


                MailMessage message = new MailMessage();
                MailAddress fromAddress = new MailAddress(_userEmail, "WorkFlow System");
                message.From = fromAddress;
                message.Subject = emailTemplate.Subject;
                message.IsBodyHtml = true;
                message.Body = emailTemplate.Body;
                message.To.Add(emailTemplate.Reciver);
                await client.SendMailAsync(message);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static async Task<bool> NotifyAboutRegistration(UserInformation user,CompanyInformation company,string reciver, string link)
        {        
            var mailModel = EmailBuilder.BuildForInvitation(user,company,link,reciver);            
            return await SendEmail(mailModel);
        }
    }
}