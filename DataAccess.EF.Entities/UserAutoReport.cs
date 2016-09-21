using System;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Entities
{
    public class UserAutoReport
    {
        public int AutoReportId { get; set; }
        public int TaskId { get; set; }
        public bool IsActive { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public string RepeatDays { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }

        public UserTask UserTask { get; set; }
    }
}
