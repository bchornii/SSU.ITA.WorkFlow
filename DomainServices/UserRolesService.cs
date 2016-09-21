using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database;
using SSU.ITA.WorkFlow.Domain.Services.DTO;

namespace SSU.ITA.WorkFlow.Domain.Services
{
    public class UserRolesService: IUserRolesService
    {
        private readonly IWorkFlowDbContextFactory _db;
        public UserRolesService(IWorkFlowDbContextFactory db, IHasher hasher)
        {
            _db = db;           
        }
        public async Task<List<string>> GetUserRoles()
        {
            List<string> roles;
            using (IWorkFlowDbContext context = _db.CreateContext())
            {
                 roles = await context.UserRole.Select(r => r.Name).ToListAsync();
            }
            return roles;
        }


        public async Task<IReadOnlyList<IRoleDto>> GetUserRolesFull()
        {
            using (IWorkFlowDbContext context = _db.CreateContext())
            {
                return await context.UserRole
                    .Where(ur => ur.Name == "Manager" || ur.Name == "Employee")
                    .Select(ur => new RoleDto
                    {
                        RoleId = ur.RoleId,
                        RoleName = ur.Name
                    }).ToListAsync();
            }
        }

    }
}
