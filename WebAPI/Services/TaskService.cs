using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Services
{
    //Класс реализующий операции с сущностью Task
    public class TaskService
    {
        public TaskService()
        {
        }


        //Метод создающий задачу с переданными аргументами
        public void CreateTask(string taskName, string about, int taskThemeId,
            int importanceId, bool isMake, string username)
        {
            var db = new ModelDB();

            db.Dispose();
            /*
            string Name { get; set; }
            string About { get; set; }
            TaskTheme TaskTheme { get; set; }
            Importance Importance { get; set; }
            bool Validation { get; set; }
            AspNetUsers User { get; set; 
            }*/
        }
    }
}