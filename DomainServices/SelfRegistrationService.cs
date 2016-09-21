using System;
using System.Data.Entity;
using System.Threading.Tasks;
using SSU.ITA.WorkFlow.Domain.Services.DTO;
using SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database;

namespace SSU.ITA.WorkFlow.Domain.Services
{
    public interface ISelfRegistrationService
    {
        Task SelfRegisterUserAsync(SelfRegistrationDto selfRegistrationDto);
        Task<bool> TokenExists(string token);
    }

    public class SelfRegistrationService : ISelfRegistrationService
    {
        private readonly IWorkFlowDbContextFactory _db;
        private readonly IHasher _hasher;

        public SelfRegistrationService(IWorkFlowDbContextFactory db,IHasher hasher)
        {
            _db = db;
            _hasher = hasher;
        }
        
        public async Task SelfRegisterUserAsync(SelfRegistrationDto selfRegistrationDto)
        {
            using (IWorkFlowDbContext context = _db.CreateContext())
            {
                if (selfRegistrationDto == null)
                {
                    throw new ArgumentNullException();
                }

                var token = await context.RegistrationToken
                    .FirstOrDefaultAsync(rt => rt.Token == selfRegistrationDto.RegistrationToken);

                if (token == null)
                {
                    throw new ArgumentNullException();
                }

                string salt = _hasher.GenerateSalt();
                string passwordHash = _hasher.ComputeHash(selfRegistrationDto.Password, salt);

                var userId = token.UserId;

                var user = await context.UserInformation
                    .FirstOrDefaultAsync(ui => ui.UserId == userId);

                if (user != null)
                {
                    user.Password = passwordHash;
                    user.Salt = salt;
                    user.Name = selfRegistrationDto.FirstName;
                    user.SurName = selfRegistrationDto.LastName;
                    user.Address = selfRegistrationDto.Address;
                    user.PhoneNumber = selfRegistrationDto.PhoneNumber;
                    user.IsConfirmed = true;
                    context.RegistrationToken.Remove(token);
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }

        public async Task<bool> TokenExists(string token)
        {
            using (IWorkFlowDbContext context = _db.CreateContext())
            {
                if (token == null)
                {
                    throw new ArgumentNullException();
                }

                var existingToken = await context.RegistrationToken
                    .FirstOrDefaultAsync(rt => rt.Token == token);

                return existingToken != null;
            }
        }
    }
}