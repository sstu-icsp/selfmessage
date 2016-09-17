
using System.Data.Entity;
using WebAPI.Models.Entities;

namespace WebAPI.Models
{
    //Класс для создания таблицы
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("SelfMessage")
        {

        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}