using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAPI.Models.Entities;

namespace WebAPI.Models
{
    public class SelfmessageContext : DbContext
    {
        public SelfmessageContext() : base("SelfMessage") { }

        public  DbSet<AspNetRoles> AspNetRoles { get; set; }
        public  DbSet<AspNetUsers> AspNetUsers { get; set; }
        public  DbSet<Notes> Notes { get; set; }
        public  DbSet<Schedules> Schedules { get; set; }
        public  DbSet<Tags> Tags { get; set; }
        public  DbSet<Tasks> Tasks { get; set; }

    }
}