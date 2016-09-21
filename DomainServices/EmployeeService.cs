using SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database;
using SSU.ITA.WorkFlow.Domain.Services.DTO;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;

namespace SSU.ITA.WorkFlow.Domain.Services
{
    public interface IEmployeeService
    {
        Task<IReadOnlyList<IEmployeeDto>> FetchEmployeeNames(int managerId);
        Task<IReadOnlyList<IProjectDto>> FetchEmployeeProjectsList(int userId);
        Task<UserInfoDto> FetchEmployeeInfo(int userId);
        Task UpdateEmployeeInfo(UserInfoDto userToUpdate);
        Task<IPagerListDto> FetchEmployeeProjects(int userId, int pageNum, int numPerPage);
    }

    public class EmployeeService : IEmployeeService
    {
        private IWorkFlowDbContextFactory _db;
        public EmployeeService(IWorkFlowDbContextFactory db)
        {
            _db = db;
        }
        public async Task<IReadOnlyList<IEmployeeDto>> FetchEmployeeNames(int managerId)
        {
            using (IWorkFlowDbContext _context = _db.CreateContext())
            {
                return await _context.UserInformation
                                     .Where(t => t.IsConfirmed &&
                                                 t.ManagerId == managerId)
                                     .Select(t => new EmployeeInitialsDto
                                     {
                                         UserId = t.UserId,
                                         FirstName = t.Name,
                                         SecondName = t.SurName
                                     })
                                     .ToListAsync();
            }
        }
        public async Task<UserInfoDto> FetchEmployeeInfo(int userId)
        {
            using (IWorkFlowDbContext _context = _db.CreateContext())
            {
                return await _context.UserInformation
                    .Where(u => u.UserId == userId)
                    .Select(u => new UserInfoDto
                    {
                        UserId = u.UserId,
                        Name = u.Name,
                        SurName = u.SurName,
                        Address = u.Address,
                        PhoneNumber = u.PhoneNumber,
                        Email = u.Email,
                        RoleId = u.RoleId,
                        RoleName = _context.UserRole
                            .Where(r => r.RoleId == u.RoleId)
                            .Select(r => r.Name).FirstOrDefault()
                    })
                    .FirstOrDefaultAsync();
            }
        }
        public async Task UpdateEmployeeInfo(UserInfoDto userToUpdate)
        {
            using (IWorkFlowDbContext context = _db.CreateContext())
            {
                UserInformation user = context.UserInformation
                    .Find(userToUpdate.UserId);

                user.Name = userToUpdate.Name;
                user.SurName = userToUpdate.SurName;
                user.RoleId = userToUpdate.RoleId;

                await context.SaveChangesAsync();
            }
        }
        
        public async Task<IPagerListDto> FetchEmployeeProjects(int userId, int pageNum, int numPerPage)
        {
            using (IWorkFlowDbContext _context = _db.CreateContext())
            {                
                int totalProjects = await _context.UserProjectRelation
                                                  .Where(upr => upr.UserId == userId)
                                                  .CountAsync();

                var projectItems = await _context.UserProject
                                                 .Where(p => p.UserProjectRelation
                                                              .Any(r => r.UserId == userId))
                                                 .OrderBy(p => p.ProjectId)
                                                 .Skip((pageNum - 1) * numPerPage)
                                                 .Take(numPerPage)
                                                 .Select(p => new ProjectWithTasksDto
                                                 {
                                                     ProjectId = p.ProjectId,
                                                     ProjectName = p.Name,
                                                     CreateDate = p.CreateDate,
                                                     Tasks = p.UserTask
                                                              .Where(t => t.UserId == userId)
                                                              .Select(t => new TaskNameDto
                                                              {
                                                                  TaskId = t.TaskId,
                                                                  TaskName = t.Name
                                                              })

                                                 })
                                                 .ToListAsync();

                return new PagerListDto
                {
                    TotalCount = totalProjects,
                    Items = projectItems
                };
            }
        }
        public async Task<IReadOnlyList<IProjectDto>> FetchEmployeeProjectsList(int userId)
        {
            using (IWorkFlowDbContext _context = _db.CreateContext())
            {
                return await _context.UserProject
                                     .Where(p => p.UserProjectRelation
                                                  .Any(r => r.UserId == userId))
                                     .Select(p => new ProjectNameDto
                                     {
                                         ProjectId = p.ProjectId,
                                         Name = p.Name,
                                     })
                                     .ToListAsync();
            }
        }
    }
}
