﻿using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Models.DTO.Task;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/tasks")]
    [Authorize]
    public class TasksController : ApiController
    {
        private readonly ModelDB _db = new ModelDB();


        //POST: api/tasks
        //Метод создания задачи
        [HttpPost]
        [Route("")]
        public HttpResponseMessage CreateTask(TaskBindingModel task)
        {
            //Проверяем указал ли пользователь тело запроса
            if (task == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Пустое тело запроса");
            }

            //Проверяем правильность пришедшей модели
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
 
            try
            {
                TaskService taskService = new TaskService(User);

                taskService.CreateTask(task.Name, task.About, task.TaskTheme, task.ImportanceId);
                return Request.CreateResponse(HttpStatusCode.OK, "Задача успешно добавлена");
            }
            catch (AlreadyExistsException)
            {
                //Если в базе данных уже существует задача с таким именем
                return Request.CreateResponse(HttpStatusCode.Conflict, "Тема задачи с таким именем уже существует");
            }

        }


        //GET: api/tasks/{id}
        //Метод получения задачи по id
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetTask(int id)
        {
            try
            {
                //Получаем задачу
                var task = new TaskService().GetTask(id);

                return Request.CreateResponse(HttpStatusCode.OK, task);
            }
            catch (TaskNotExistsException e)
            {
                //Если задача не найдена
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        //GET: api/tasks/{id}
        //Метод получения задач пользователя
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetTaskByUser()
        {
            try
            {
                //Получаем задачи пользователя
                var task = new TaskService(User).GetTaskByUser();

                return Request.CreateResponse(HttpStatusCode.OK, task);
            }
            catch (TaskNotExistsException e)
            {
                //Если задача не найдена
                return Request.CreateResponse(HttpStatusCode.NotFound, e.Message);
            }
        }

        //PUT: api/tasks/{id}/validate
        //Метод для указания, что задача выполнена
        [HttpPut]
        [Route("{id}/validate")]
        public HttpResponseMessage Validate(int id)
        {
            try
            {
                //Помечаем задачу как выполненную
                 new TaskService().Validate(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (TaskNotExistsException e)
            {
                //Если задача не найдена
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        //PUT: api/tasks/{id}/tasktheme
        //Метод для изменения темы задачи
        [HttpPut]
        [Route("{id}/tasktheme")]
        public HttpResponseMessage TaskThemeEdit(int id,TaskThemeEdit taskThemeEdit)
        {
            try
            {
                //Помечаем задачу как выполненную
                new TaskService().TaskThemeEdit(id, taskThemeEdit.TaskTheme);

                return Request.CreateResponse(HttpStatusCode.OK,"Тема записи изменена");
            }
            catch (TaskNotExistsException e)
            {
                //Если задача не найдена
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }


        //PUT: api/tasks/{id}/startdate
        //Метод для указания времени начала выполнения задачи
        [HttpPut]
        [Route("{id}/starttime")]
        public HttpResponseMessage StartTimeEdit(int id, StartTimeEdit startDateEdit)
        {
            try
            {
                //Помечаем задачу как выполненную
                new TaskService().StartDateEdit(id, startDateEdit.StartDate);

                return Request.CreateResponse(HttpStatusCode.OK, "Время начала задачи добавлено");
            }
            catch (TaskNotExistsException e)
            {
                //Если задача не найдена
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        //PUT: api/tasks/{id}/endtime
        //Метод для указания времени окончания выполнения задачи
        [HttpPut]
        [Route("{id}/endtime")]
        public HttpResponseMessage EndTimeEdit(int id, EndTimeEdit endTimeEdit)
        {
            try
            {
                //Помечаем задачу как выполненную
                new TaskService().EndTimeEdit(id, endTimeEdit.EndTime);

                return Request.CreateResponse(HttpStatusCode.OK, "Время окончания задачи добавлено");
            }
            catch (TaskNotExistsException e)
            {
                //Если задача не найдена
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        //DELETE: api/tasks/{id}
        //Метод удаления задачи
        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeleteTask(int id)
        {
            //Проверяем правильность пришедшей модели
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                //удаляем задачу
                new TaskService().DeleteTask(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Задача с id = " + id + " удалена из базы данных");
            }
            catch (TaskNotExistsException)
            {
                //Если задачи не существует в базе данных
                return Request.CreateResponse(HttpStatusCode.BadRequest,"Задачи с таким ид не существует в базе данных");
            }
        }

        //PUT: api/tasks/{id}
        //Метод для изменения названия, описания и важности модели
        [HttpPut]
        [Route("{id}")]
        public HttpResponseMessage EditTask(int id, EditModel editModel)
        {
            try
            {
                //Помечаем задачу как выполненную
                new TaskService().EditTask(id,editModel.Name,editModel.About,editModel.ImportanceId);

                return Request.CreateResponse(HttpStatusCode.OK,"Запись изменена");
            }
            catch (TaskNotExistsException e)
            {
                //Если задача не найдена
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}