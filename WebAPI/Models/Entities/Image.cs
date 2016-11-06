using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public ICollection<Note> Notes { get; set; }

        public Image()
        {
            Notes = new List<Note>();
        }
    }
}