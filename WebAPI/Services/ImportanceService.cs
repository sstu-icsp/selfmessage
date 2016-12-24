using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Exceptions;
using WebAPI.Exceptions.Importance;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Entities;

namespace WebAPI.Services
{
    public class ImportanceService
    {
        //Метод для получения моделей всех важностей
        public List<ImportanceViewModel> GetAllImportances()
        {
            //Открываем соеденение с базой данных
            var db = new ModelDB();

            //Формируем список моделей на основе важностей из базы данных
            var result = db.Importances
                .Select(task => new ImportanceViewModel { Id = task.Id, Name = task.Name }).ToList();

            //Закрываем соеденение с базой данных
            db.Dispose();

            //Отправляем полученный выше список моделей
            return result;
        }

        //Метод для поулчения модели важности по id
        public ImportanceViewModel GetImportance(int id)
        {
            //Открываем соеденение с базой данных
            var db = new ModelDB();

            try
            {
                //Получаем сущность темы задач по id
                var importance = db.Importances.Find(id);

                //Проверяем получили ли мы сущность
                if (importance == null)
                {
                    throw new ImportanceNotExistsException("Важности с таким ид не существует");
                }

                //Преобразуем сущность в модель
                var result = new ImportanceViewModel
                {
                    Id = importance.Id,
                    Name = importance.Name
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

        //Метод для создания новой важности
        public void CreateImportance(string name)
        {
            //Открываем соеденение с базой данных
            var db = new ModelDB();

            try
            {
                //Проверяем существует ли такая тема задачи
                if (db.Importances.Any(p => p.Name.Equals(name)))
                {
                    throw new AlreadyExistsException("Важности с таким именем уже существует");
                }

                //Добавляем важность
                db.Importances.Add(
                    new Importance { Name = name }
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
        public void UpdateImportance(int id, ImportanceBindingModel importanceBinding)
        {
            var db = new ModelDB();

            try
            {
                var importance = db.Importances.Find(id);

                if (importance == null)
                {
                    throw new ImportanceNotExistsException("Важности с таким ИД не существует");
                }

                if (db.Importances.Any(p => p.Name == importanceBinding.Name))
                {
                    throw new AlreadyExistsException("Важность с таким названием уже существует");
                }

                importance.Name = importanceBinding.Name;

                db.SaveChanges();
            }
            finally
            {
                db.Dispose();
            }
        }

        //Метод для удаление тем задач
        public void DeleteImportance(int id)
        {
            //Открываем соеденение с базой данных
            var db = new ModelDB();

            try
            {
                var importanceForRemove = db.Importances.Find(id);

                if (importanceForRemove == null)
                {
                    throw new ImportanceNotExistsException("Важность с таким ид не существует");
                }

                db.Importances.Remove(importanceForRemove);

                db.SaveChanges();
            }
            finally
            {
                db.Dispose();
            }
        }
    }
}