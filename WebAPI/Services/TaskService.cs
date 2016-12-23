using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Models.Entities;
using WebAPI.Workers;

namespace WebAPI.Services
{
    //Класс реализующий операции с сущностью Task
    public class TaskService
    {
        private readonly IPrincipal _user;

        public TaskService(IPrincipal user)
        {
            _user = user;
        }


        //Метод создающий задачу с переданными аргументами
        public void CreateTask(string taskName, string about, int taskThemeId,
            int importanceId,DateTime? startTime, DateTime? endTime)
        {
            var db = new ModelDB();
            try
            {
                //Проверяем существует ли такая задача
                if (db.Tasks.Any(p => p.Name.Equals(taskName)))
                {
                    throw new AlreadyExistsException("Тема задачи с таким именем уже существует");
                }


                var taskTheme = db.TaskThemes.FirstOrDefault(p => p.Id == taskThemeId);
                var importance = db.Importances.FirstOrDefault(p => p.Id == importanceId);
                var user = UserWorker.GetUserByName(_user.Identity.Name, db);

                //Создаем задачу
                Task task = new Task
                {
                    Name = taskName,
                    About = about,
                    TaskTheme = taskTheme,
                    Importance = importance,
                    StartTime = startTime,
                    EndTime = endTime,
                    DateAdded = DateTime.Now,
                    Validation = false,
                    User = user
                };

                db.Tasks.Add(task);
                db.SaveChanges();
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}