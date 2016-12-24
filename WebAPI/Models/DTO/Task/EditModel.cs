using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models.DTO
{
    public class EditModel
    {
        public string Name { get; set; }
        public string About { get; set; }
        public int ImportanceId { get; set; }
    }
}