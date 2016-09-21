using System;
using System.Data.Entity;
using System.Threading.Tasks;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;
using SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database;
using SSU.ITA.WorkFlow.Domain.Services.DTO;
using System.Collections.Generic;

namespace SSU.ITA.WorkFlow.Domain.Services
{
    public interface IAccountService
    {
        Task CreateCompanyAsync(RegisterDto registrationDto);
        Task CreateUserAsync(RegisterDto registrationDto);
        Task<CompanyInformation> FindCompanyByName(string companyName);
        Task<UserInformation> FindUserByEmail(string userEmail);
        Task<string> GetSaltAsync(string userEmail);
        Task<CompanyInformation> FindCompanyById(int id);
        Task<UserInformation> FindUser(string userName, string hash);
        Task<List<UserRole>> GetRolesList();
    }

    public class AccountService : IAccountService
    {
        private const int ManagerRoleId = 2;
        private const int DefaultManagerId = 1;
        private readonly IWorkFlowDbContextFactory _db;
        private readonly IHasher _hasher;
        public AccountService(IWorkFlowDbContextFactory db, IHasher hasher)
        {
            _db = db;
            _hasher = hasher;
        }

        public async Task CreateCompanyAsync(RegisterDto registrationDto)
        {
            using (IWorkFlowDbContext context = _db.CreateContext())
            {
                if (registrationDto == null)
                {
                    throw new ArgumentNullException();
                }

                context.CompanyInformation.Add(new CompanyInformation
                {
                    Name = registrationDto.CompanyName
                });
                await context.SaveChangesAsync();
            }
        }

        public async Task CreateUserAsync(RegisterDto registrationDto)
        {
            using (IWorkFlowDbContext context = _db.CreateContext())
            {
                if (registrationDto == null)
                {
                    throw new ArgumentNullException();
                }

                var company = await FindCompanyByName(registrationDto.CompanyName);
                string salt = _hasher.GenerateSalt();
                string passwordHash = _hasher.ComputeHash(registrationDto.Password, salt);

                context.UserInformation.Add(new UserInformation
                {
                    RoleId = ManagerRoleId,
                    CompanyId = company.CompanyId,
                    ManagerId = DefaultManagerId,
                    Email = registrationDto.Email,
                    PhoneNumber = registrationDto.Phone,
                    Name = registrationDto.FirstName,
                    SurName = registrationDto.LastName,
                    Password = passwordHash,
                    Salt = salt,
                    IsConfirmed = true
                });
                await context.SaveChangesAsync();               
            }
            await UpdateUser(registrationDto.Email);
        }

        public async Task<CompanyInformation> FindCompanyByName(string companyName)
        {
            using (IWorkFlowDbContext context = _db.CreateContext())
            {
                var company = await context.CompanyInformation
                    .FirstOrDefaultAsync(ci => ci.Name == companyName);
                return company;
            }
        }

        public async Task<CompanyInformation> FindCompanyById(int id)
        {
            using (IWorkFlowDbContext context = _db.CreateContext())
            {
                var company = await context.CompanyInformation
                    .FirstOrDefaultAsync(ci => ci.CompanyId == id);
                return company;
            }
        }

        public async Task UpdateUser(string userEmail)
        {
            using (var context = _db.CreateContext())
            {
                var user = await context.UserInformation
                    .FirstOrDefaultAsync(ui => ui.Email == userEmail);
                if (user != null)
                {
                    user.ManagerId = user.UserId;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<UserInformation> FindUserByEmail(string userEmail)
        {
            using (IWorkFlowDbContext context = _db.CreateContext())
            {
                var user = await context.UserInformation
                    .FirstOrDefaultAsync(ui => ui.Email == userEmail);
                return user;
            }
        }

        public async Task<UserInformation> FindUser(string userEmail, string password)
        {
            using (IWorkFlowDbContext context = _db.CreateContext())
            {
                if (await FindUserByEmail(userEmail) != null)
                {
                    var salt = await GetSaltAsync(userEmail);
                    var hash = _hasher.ComputeHash(password, salt);
                    var user = await context.UserInformation
                        .FirstOrDefaultAsync(ui =>
                        ui.Email == userEmail && ui.Password == hash);
                    return user;
                }              
                    return null;               
            }
        }

        public async Task<string> GetSaltAsync(string userEmail)
        {
            UserInformation user = await FindUserByEmail(userEmail);
            return user.Salt;
        }

        public async Task<List<UserRole>> GetRolesList()
        {
            using (IWorkFlowDbContext context = _db.CreateContext())
            {
                List<UserRole> roles = await context.UserRole.ToListAsync();
                return roles;
            }
        }
    }
}
