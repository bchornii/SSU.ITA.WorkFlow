using System;
using System.ComponentModel.DataAnnotations;

namespace SSU.ITA.WorkFlow.Domain.Services.DTO
{
    public class ReportDto
    {
        public int ReportId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int TaskId { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        public string Comment { get; set; }
    }
}
