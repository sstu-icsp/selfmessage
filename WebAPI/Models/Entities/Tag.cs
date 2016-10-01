using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.Test
{
    public class Tag
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public AspNetUsers User { get; set; }
        public ICollection<Note> Notes { get; set; }

        public Tag()
        {
            Notes = new List<Note>();
        }
    }
}