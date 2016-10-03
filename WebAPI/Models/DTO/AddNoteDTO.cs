using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.DTO
{
    public class AddNoteDTO
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}