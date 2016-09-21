using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using EmailServiceWorkFLow;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;
using SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database;
using SSU.ITA.WorkFlow.Domain.Services.DTO;

namespace SSU.ITA.WorkFlow.Domain.Services
{
    public interface IInvitationService
    {
        Task<bool> SaveInvitedUsers(InvitationDto invitation);
        IEnumerable<string> ParseUsers(string emails);
    }

    public class InvitationService : IInvitationService
    {
        private readonly IRegistrationTokenService _rservice;
        private readonly IAccountService _aservice;
        private readonly IWorkFlowDbContextFactory _db;

        public InvitationService(IRegistrationTokenService rservice,IAccountService aservice, IWorkFlowDbContextFactory db)
        {
            _rservice = rservice;
            _aservice = aservice;
            _db = db;
        }

        public IEnumerable<string> ParseUsers(string emails)
        {          
            List<string> parsedEmails = new List<string>();           
            for (int i = 0; i < emails.Length; i++)
            {
                char a = emails[i];
                List<char> email = new List<char>();
                while (a != ';'&&i!= emails.Length-1)
                {
                    if (a != ' ')
                    {
                        email.Add(a);
                    }
                    i++;
                    a = emails[i];                    
                }
                if (a != ';' && a != ' ')
                {
                    email.Add(a);
                }
                parsedEmails.Add(new string(email.ToArray()));
            }
            return parsedEmails;
        }

        public async Task<bool> SaveInvitedUsers(InvitationDto invitation)
        {
            var user = await _aservice.FindUserByEmail(invitation.EmployerEmail);  
            var company = await _aservice.FindCompanyById(user.CompanyId);
                 
            var newUsers = ParseUsers(invitation.EmployeesEmails);           
            foreach (string newUserEmail in newUsers)
            {
                if (await _aservice.FindUserByEmail(newUserEmail) != null)
                {
                    return true;
                }
            }
            using (var context = _db.CreateContext())
            {
                var role = await context.UserRole.FirstAsync(t => t.Name == invitation.RoleName);
                foreach (var email in newUsers)
                {
                    context.UserInformation.Add(new UserInformation
                    {
                        RoleId = role.RoleId,
                        CompanyId = user.CompanyId,
                        ManagerId = user.UserId,
                        Email = email,
                        Password = " ",
                        Salt = " ",
                        IsConfirmed = false
                    });
                    
                }
                await context.SaveChangesAsync();
            }
            foreach (string newUserEmail in newUsers)
            {
                var newUser = await _aservice.FindUserByEmail(newUserEmail);
                var token = _rservice.GenerateToken();
                await _rservice.SaveToken(newUser.UserId, token);
                await EmailService.NotifyAboutRegistration(user, company, newUserEmail, "http://localhost:4446/logic/SelfRegistration?token=" + token);
            }
            return false;
        }
    }
}
