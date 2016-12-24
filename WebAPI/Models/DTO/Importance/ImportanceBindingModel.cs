using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI.Models.DTO
{
    public class ImportanceBindingModel
    {
        [Required]
        [Display(Name = "Название важности")]
        public string Name { get; set; }
    }
}