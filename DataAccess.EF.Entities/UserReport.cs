using System;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Entities
{
    public class UserReport
    {
        public int ReportId { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Comment { get; set; }

        public UserTask UserTask { get; set; }
        public UserInformation UserInformation { get; set; }
    }
}
