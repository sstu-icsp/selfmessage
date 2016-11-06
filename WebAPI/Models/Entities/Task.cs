using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? DateAdded { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public TaskTheme TaskTheme { get; set; }
        public ICollection<Note> Notes {get;set;}
        public Importance Importance { get; set; }
        public bool Validation { get; set; }
        public AspNetUsers User { get; set; }

        public Task()
        {
            Notes = new List<Note>();
            Validation = false;
        }
    }
}