using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.Entities
{
    //Класс создания таблицы Теги
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public ICollection<Note> Notes { get; set; }

        public Tag()
        {
            Notes = new List<Note>();
        }
        
    }
}