using SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database;
using SSU.ITA.WorkFlow.Domain.Services.DTO;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;

namespace SSU.ITA.WorkFlow.Domain.Services
{
    public interface ITaskService
    {
        Task<ITaskDto> FetchEmployeeTask(int taskId);
        Task<IReadOnlyList<WorkItemStatusDto>> FetchTaskStatuses();
        Task CreateOrUpdateTaskInformation(TaskDto taskObject);
    }
    public class TasksService : ITaskService
    {
        private IWorkFlowDbContextFactory _db;
        public TasksService(IWorkFlowDbContextFactory db)
        {
            _db = db;
        }
        public async Task<ITaskDto> FetchEmployeeTask(int taskId)
        {
            using (IWorkFlowDbContext _context = _db.CreateContext())
            {
                return await _context.UserTask
                                     .Where(t => t.TaskId == taskId)
                                     .Select(t => new TaskDescriptionDto
                                     {
                                         TaskId = taskId,
                                         TaskName = t.Name,
                                         ProjectId = t.ProjectId,
                                         ProjectName = t.UserProject.Name,
                                         StatusId = t.StatusId,
                                         StatusName = t.TaskStatus.Name,
                                         Description = t.Description,
                                         EmployeeId = t.UserId,
                                         EmployeeFirstName = t.UserInformation.Name,
                                         EmployeeSecondName = t.UserInformation.SurName                                     
                                     })
                                     .FirstOrDefaultAsync();
            }

        }        
        public async Task<IReadOnlyList<WorkItemStatusDto>> FetchTaskStatuses()
        {
            using (IWorkFlowDbContext _context = _db.CreateContext())
            {
                return await _context.TaskStatus
                                     .Select(s => new WorkItemStatusDto
                                     {
                                         Id = s.StatusId,
                                         Name = s.Name
                                     })
                                     .ToListAsync();
            }
        }
        public async Task CreateOrUpdateTaskInformation(TaskDto taskObject)
        {
            using (IWorkFlowDbContext _context = _db.CreateContext())
            {
                UserTask task = new UserTask
                {
                    TaskId = taskObject.TaskId,
                    Name = taskObject.TaskName,
                    ProjectId = taskObject.ProjectId,
                    UserId = taskObject.UserId,
                    StatusId = taskObject.StatusId,
                    Description = taskObject.Description
                };
                _context.Entry(task).State = (taskObject.TaskId != 0) ?
                                                            EntityState.Modified :
                                                            EntityState.Added;
                await _context.SaveChangesAsync();
            }
        }
    }
}
