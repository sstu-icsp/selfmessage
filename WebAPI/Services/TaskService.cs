using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Models.DTO.Task;
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

        public TaskService()
        {
            _user = null;
        }


        //Метод создающий задачу с переданными аргументами
        public void CreateTask(string taskName, string about, string taskThemeName,
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


                var taskTheme = db.TaskThemes.FirstOrDefault(p => p.Name== taskThemeName);

                if (taskTheme == null)
                {
                    taskTheme = new TaskTheme { Name = taskThemeName };
                    db.TaskThemes.Add(taskTheme);
                    db.SaveChanges();  
                }

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

        //Метод для удаления задач
        public void DeleteTask(int id)
        {
            //Открываем соеденение с базой данных
            var db = new ModelDB();

            try
            {
                var taskForRemove = db.Tasks.Find(id);

                if (taskForRemove == null)
                {
                    throw new TaskNotExistsException("Тема задачи с таким ид не существует");
                }

                db.Tasks.Remove(taskForRemove);

                db.SaveChanges();
            }
            finally
            {
                db.Dispose();
            }
        }

        //Метод для поулчения модели задач по ИД
        public TaskViewModel GetTask(int id)
        {
            //Открываем соеденение с базой данных
            var db = new ModelDB();

            try
            {
                //Получаем сущность задачи по ид
                var task = db.Tasks.Include("Importance").Include("TaskTheme").FirstOrDefault(p=>p.Id==id);

                //Проверяем получили ли мы сущность
                if (task == null)
                {
                    throw new TaskNotExistsException("Задачи с таким ид не существует");
                }

                return ConvertFromTaskInTaskViewModel(task);
            }
            finally
            {
                //Закрываем соеденение с базой данных
                db.Dispose();
            }
        }

        //Метод для пометки задачи как выполненной
        public void Validate(int id)
        {
            //Открываем соеденение с базой данных
            var db = new ModelDB();

            try
            {
                //Получаем сущность задачи по ид
                var task = db.Tasks.Find(id);

                //Проверяем получили ли мы сущность
                if (task == null)
                {
                    throw new TaskNotExistsException("Задачи с таким ид не существует");
                }

                task.Validation = true;
                db.SaveChanges();
            }
            finally
            {
                //Закрываем соеденение с базой данных
                db.Dispose();
            }
        }

        public void TaskThemeEdit(int id,string taskThemeName)
        {
            //Открываем соеденение с базой данных
            var db = new ModelDB();
            try
            {
                var task = db.Tasks.Find(id);

                if (task == null)
                {
                    throw new TaskNotExistsException("Задачи с таким ид не существует");
                }

                var taskTheme = db.TaskThemes.FirstOrDefault(p => p.Name == taskThemeName);

                if (taskTheme == null)
                {
                    taskTheme = new TaskTheme { Name = taskThemeName };
                    db.TaskThemes.Add(taskTheme);
                    db.SaveChanges();
                }

                task.TaskTheme = taskTheme;
                db.SaveChanges();

            }
            finally
            {
                db.Dispose();
            }

            
        }

        //Метод для поулчения модели задач по ИД
        public IEnumerable<TaskViewModel> GetTaskByUser()
        {
            //Открываем соеденение с базой данных
            var db = new ModelDB();

            try
            {
                var username = UserWorker.GetUserByName(_user.Identity.Name, db).UserName;
                //Получаем сущность задачи по ид
                var tasks = db.Tasks.Include("Importance").Include("TaskTheme").Include("User").Where(p => p.User.UserName==username).ToList();

                var tasksViewModel = new List<TaskViewModel>();

                foreach(Task task in tasks){
                    tasksViewModel.Add(ConvertFromTaskInTaskViewModel(task));
                }

                return tasksViewModel;
            }
            finally
            {
                //Закрываем соеденение с базой данных
                db.Dispose();
            }
        }

        public static TaskViewModel ConvertFromTaskInTaskViewModel(Task task)
        {
            //Преобразуем сущность в модель
            var result = new TaskViewModel
            {
                Id = task.Id,
                Name = task.Name,
                About = task.About,
                DateAdded = task.DateAdded,
                EndTime = task.EndTime,
                ImportanceId = task.Importance.Id,
                ImportanceName = task.Importance.Name,
                StartTime = task.StartTime,
                TaskThemeId = task.TaskTheme.Id,
                TaskThemeName = task.TaskTheme.Name,
                Validation = task.Validation
            };

            //Отправляем полученную выше модель
            return result;
        }
    }
}