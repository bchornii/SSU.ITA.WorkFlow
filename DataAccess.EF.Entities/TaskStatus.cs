using System.Collections.Generic;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Entities
{
    public class TaskStatus
    {
        public int StatusId { get; set; }
        public string Name { get; set; }

        public ICollection<UserTask> UserTask { get; set; }

        public TaskStatus()
        {
            UserTask = new List<UserTask>();
        }
    }
}
