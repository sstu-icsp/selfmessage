using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.DTO.Task
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? DateAdded { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string TaskThemeName { get; set; }
        public int TaskThemeId { get; set; }
        public string ImportanceName { get; set; }
        public int ImportanceId { get; set; }
        public bool Validation { get; set; }
    }
}