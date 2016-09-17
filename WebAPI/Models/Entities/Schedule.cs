using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public ICollection<Task> Tasks { get; set; }

        public Schedule()
        {
            Tasks = new List<Task>();
        }
    }
}