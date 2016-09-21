using System.Collections.Generic;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Entities
{
    public class ProjectStatus
    {
        public int StatusId { get; set; }
        public string Name { get; set; }

        public ICollection<UserProject> UserProject { get; set; }

        public ProjectStatus()
        {
            UserProject = new List<UserProject>();
        }
    }
}
