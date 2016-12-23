using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models.DTO.Task
{
    public class TaskBindingModel
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string About { get; set; }

        [Required]
        public string TaskTheme { get; set; }

        [Required]
        public int ImportanceId { get; set; }
    }
}