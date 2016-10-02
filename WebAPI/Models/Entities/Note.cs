﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime DataAdded { get; set; }
        public AspNetUsers User { get; set; }
        public ICollection<Tag> Tags { get; set; }

        public Note ()
        {
            Tags = new List<Tag>();
        }
    }
}