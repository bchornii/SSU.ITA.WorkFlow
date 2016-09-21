using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;
using SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database;

namespace SSU.ITA.WorkFlow.Domain.Services
{
    public interface IRegistrationTokenService
    {
        string GenerateToken();
        Task SaveToken(int userId, string token);
    }

    public class RegistrationTokenService : IRegistrationTokenService
    {
        private readonly IWorkFlowDbContextFactory _db;

        public RegistrationTokenService(IWorkFlowDbContextFactory db)
        {
            _db = db;
        }

        public string GenerateToken()
        {
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            token = (new Regex("[/+=]")).Replace(token, "");
            return token;
        }

        public async Task SaveToken(int userId, string token)
        {
            using (IWorkFlowDbContext context = _db.CreateContext())
            {
                context.RegistrationToken.Add(new RegistrationToken
                {
                    Token = token,
                    UserId = userId
                });
                await context.SaveChangesAsync();              
            }
        }
    }
}
