using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.DTO.Note
{
    public class InputNoteDto
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public string Tags { get; set; }
    }
}