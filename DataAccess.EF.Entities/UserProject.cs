using System;
using System.Collections.Generic;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Entities
{
    public class UserProject
    {
        public int ProjectId { get; set; }
        public int StatusId { get; set; }
        public string Name { get; set; }
        public int CreatorId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public ICollection<UserProjectRelation> UserProjectRelation { get; set; }
        public ICollection<UserTask> UserTask { get; set; }

        public ProjectStatus ProjectStatus { get; set; }

        public UserProject()
        {
            UserProjectRelation = new List<UserProjectRelation>();
            UserTask = new List<UserTask>();
        }
    }
}
