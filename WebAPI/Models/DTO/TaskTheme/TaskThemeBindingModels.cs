using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models.DTO
{
    public class TaskThemeBindingModels
    {
        [Required]
        [Display(Name = "Название темы задачи")]
        public string Name { get; set; }
    }
}