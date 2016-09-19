﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebAPI.Models.Entities
{
    //Класс для создания таблицы Записи
    
    public class Note
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }

        //Добавлено поле
        public string Name { get; set; }

        
        public ICollection<Tag> Tags { get; set; }

        public ICollection<Task> Tasks { get; set; }
        public Note()
        {
            Tags = new List<Tag>();
            Tasks = new List<Task>();
        }         
    }
}