using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.DTO
{
    public class NoteDTO
    {
        public NoteDTO()
        {
            Tags = new List<TagDTO>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public List<TagDTO> Tags { get; set; }
    }
}