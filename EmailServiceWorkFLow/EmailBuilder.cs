using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;

namespace EmailServiceWorkFLow
{
    public static class EmailBuilder
    {
        public static EmailTemplateModel BuildForInvitation(UserInformation user,CompanyInformation company,string link, string reciver)
        {

            var emailTemplate = new EmailTemplateModel();
            try
            {
                var body = string.Format(
                    "<table><tbody>" +
                    "<tr><td>Title</td><td>{0}</td></tr>" +
                    "<tr><td>Description</td><td>{1}</td></tr>" +
                    "<tr><td>Registration Link</td><td>{2}</td></tr>" +                   
                    "</tbody></table>", user.Name +"  " + user.SurName + " invites you for working in "+company.Name, "If you have some questions please write your employer :" + user.Email,
                    "To accept invitation pls follow this link an register : "+link);
                emailTemplate.Reciver = reciver;
                emailTemplate.Body = body;
                emailTemplate.Subject = "Invitation for WorkFlow time tracking system";
            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw;
            }

            return emailTemplate;
        }
    }
}
