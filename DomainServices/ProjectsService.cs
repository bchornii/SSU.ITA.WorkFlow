using SSU.ITA.WorkFlow.DataAccess.EF.Infrastructure.Database;
using SSU.ITA.WorkFlow.Domain.Services.DTO;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using SSU.ITA.WorkFlow.DataAccess.EF.Repository.Repositories;

namespace SSU.ITA.WorkFlow.Domain.Services
{
    public interface IProjectsService
    {
        Task<IReadOnlyCollection<ProjectNameDto>> GetProjects(int managerId);
        Task<ProjectDto> GetProject(int id);
        Task UpdateProject(ProjectDto project);
        Task<int> CreateNewProject(ProjectDto project);
        Task<IReadOnlyCollection<EmployeeInitialsDto>> GetEmployees(int managerId, int[] EmployeesId);
    }

    public class ProjectsService : IProjectsService
    {
        private readonly IWorkFlowDbContextFactory _db;
        private readonly IProjectRepository _projectRepository;

        public ProjectsService(IWorkFlowDbContextFactory db, IProjectRepository projectRepository)
        {
            _db = db;
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDto> GetProject(int projectId)
        {
            using (IWorkFlowDbContext context = _db.CreateContext())
            {
                IEnumerable<EmployeeInitialsDto> projectEmployees = context.UserInformation
                  .Where(u => u.UserProjectRelation.Any(ur => ur.ProjectId == projectId))
                  .Select(u => new EmployeeInitialsDto
                  {
                      UserId = u.UserId,
                      FirstName = u.Name,
                      SecondName = u.SurName
                  });

                ProjectDto project = await context.UserProject
                  .Where(p => p.ProjectId == projectId)
                  .Select(p => new ProjectDto
                  {
                      Name = p.Name,
                      ProjectId = p.ProjectId,
                      StatusId = p.StatusId,
                      CreateDate = p.CreateDate,
                      EndDate = p.EndDate,
                      Description = p.Description,
                      Employees = projectEmployees,
                      CreatorId = p.CreatorId
                  })
                  .FirstOrDefaultAsync();

                return project;
            }
        }

        public async Task UpdateProject(ProjectDto updatedProject)
        {
            using (IWorkFlowDbContext context = _db.CreateContext())
            {
                var project = new UserProject
                {
                    ProjectId = updatedProject.ProjectId,
                    Name = updatedProject.Name,
                    StatusId = updatedProject.StatusId,
                    CreateDate = updatedProject.CreateDate,
                    EndDate = updatedProject.EndDate,
                    Description = updatedProject.Description,
                    CreatorId = updatedProject.CreatorId
                };

                context.Entry(project).State = EntityState.Modified;
                await context.SaveChangesAsync();

                IEnumerable<int> employeesId = updatedProject.Employees.Select(e => e.UserId);

                foreach (int id in employeesId)
                {
                    context.UserProjectRelation.Add(new UserProjectRelation
                    {
                        UserId = id,
                        ProjectId = updatedProject.ProjectId
                    });
                }
                await context.SaveChangesAsync();
            }
        }
        //--------------------------------
        public async Task<IReadOnlyCollection<ProjectNameDto>> GetProjects(int managerId)
        {
            IEnumerable<UserProject> projects = await _projectRepository.GetProjects(managerId);

            return projects.Select(p => new ProjectNameDto
            {
                ProjectId = p.ProjectId,
                Name = p.Name
            }).ToList();
        }

        public async Task<int> CreateNewProject(ProjectDto project)
        {
            using (IWorkFlowDbContext context = _db.CreateContext())
            {
                var newProject = new UserProject
                {   
                    StatusId = project.StatusId,
                    Name = project.Name,
                    CreateDate = project.CreateDate,
                    EndDate = project.EndDate,
                    Description = project.Description,
                    CreatorId = project.CreatorId
                };

                context.UserProject.Add(newProject);

                IEnumerable<int> employeesId = project.Employees.Select(e => e.UserId);

                foreach (int empId in employeesId)
                {
                    context.UserProjectRelation.Add(new UserProjectRelation
                    {
                        UserId = empId,
                        ProjectId = project.ProjectId
                    });
                }

                await context.SaveChangesAsync();
                return newProject.ProjectId;
            }
        }

        public async Task<IReadOnlyCollection<EmployeeInitialsDto>> GetEmployees(int managerId, int[] employeesId)
        {
            using (IWorkFlowDbContext context = _db.CreateContext())
            {
                return await context.UserInformation
                     .Where(ui => ui.ManagerId == managerId && ui.IsConfirmed && !employeesId.Contains(ui.UserId))
                     .Select(ui => new EmployeeInitialsDto
                     {
                         UserId = ui.UserId,
                         FirstName = ui.Name,
                         SecondName = ui.SurName
                     }).ToListAsync();
            }
        }
    }
}