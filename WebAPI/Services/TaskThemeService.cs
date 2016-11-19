using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Entities;

namespace WebAPI.Services
{
    public class TaskThemeService
    {
        //Метод для получения моделей всех тем задач
        public List<TaskThemeViewModel> GetAllTaskThemes()
        {
            //Открываем соеденение с базой данных
            var db = new ModelDB();

            //Формируем список моделей на основе тем задач из базы данных
            var result = db.TaskThemes
                .Select(task => new TaskThemeViewModel {Id = task.Id, Name = task.Name}).ToList();

            //Закрываем соеденение с базой данных
            db.Dispose();

            //Отправляем полученный выше список моделей
            return result;
        }

        //Метод для поулчения модели задач по ИД
        public TaskThemeViewModel GetTaskTheme(int id)
        {
            //Открываем соеденение с базой данных
            var db = new ModelDB();

            try
            {
                //Получаем сущность темы задач по ид
                var taskTheme = db.TaskThemes.Find(id);

                //Проверяем получили ли мы сущность
                if (taskTheme == null)
                {
                    throw new TaskThemeNotExistsException("Тема задачи с таким ид не существует");
                }

                //Преобразуем сущность в модель
                var result = new TaskThemeViewModel
                {
                    Id = taskTheme.Id,
                    Name = taskTheme.Name
                };

                //Отправляем полученную выше модель
                return result;
            }
            finally
            {
                //Закрываем соеденение с базой данных
                db.Dispose();
            }
        }

        //Метод для создания новой темы задачи
        public void CreateTaskTheme(string name)
        {
            //Открываем соеденение с базой данных
            var db = new ModelDB();

            try
            {
                //Проверяем существует ли такая тема задачи
                if (db.TaskThemes.Any(p => p.Name.Equals(name)))
                {
                    throw new AlreadyExistsException("Тема задачи с таким именем уже существует");
                }

                //Добавляем тему задачи
                db.TaskThemes.Add(
                    new TaskTheme {Name = name}
                );

                //Сохраняем изменения
                db.SaveChanges();
            }
            finally
            {
                //Закрываем соеднение с базой данных, так как пробрасывается исключение добавил в блок finally
                db.Dispose();
            }
        }

        //Метод для изменения данных в записи
        public void UpdateTaskTheme(int id, TaskThemeBindingModels taskThemeBinding)
        {
            var db = new ModelDB();

            try
            {
                var taskTheme = db.TaskThemes.Find(id);

                if (taskTheme == null)
                {
                    throw new TaskThemeNotExistsException("Тема задачи с таким ИД не существует");
                }

                if (db.TaskThemes.Any(p => p.Name == taskThemeBinding.Name))
                {
                    throw new AlreadyExistsException("Тема задачи с таким названием уже существует");
                }

                taskTheme.Name = taskThemeBinding.Name;

                db.SaveChanges();
            }
            finally
            {
                db.Dispose();
            }
        }

        //Метод для удаление тем задач
        public void DeleteTaskTheme(int id)
        {
            //Открываем соеденение с базой данных
            var db = new ModelDB();

            try
            {
                var taskThemForRemove = db.TaskThemes.Find(id);

                if (taskThemForRemove == null)
                {
                    throw new TaskThemeNotExistsException("Тема задачи с таким ид не существует");
                }

                db.TaskThemes.Remove(taskThemForRemove);

                db.SaveChanges();
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}