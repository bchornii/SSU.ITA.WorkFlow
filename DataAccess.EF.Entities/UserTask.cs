using System.Collections.Generic;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Entities
{
    public class UserTask
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public string Description { get; set; }

        public ICollection<UserAutoReport> UserAutoReport { get; set; }
        public ICollection<UserReport> UserReport { get; set; }

        public UserProject UserProject { get; set; }
        public UserInformation UserInformation { get; set; }
        public TaskStatus TaskStatus { get; set; }

        public UserTask()
        {
            UserAutoReport = new List<UserAutoReport>();
            UserReport = new List<UserReport>();
        }
    }
}
