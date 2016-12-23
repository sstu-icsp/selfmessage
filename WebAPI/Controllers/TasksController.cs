using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Models.DTO.Task;
using WebAPI.Models.Entities;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/tasks")]
    [Authorize]
    public class TasksController : ApiController
    {
        private readonly ModelDB _db = new ModelDB();
        private TaskService _taskService = new TaskService();


        [HttpPost]
        [Route("")]
        [Authorize]
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

                taskService.CreateTask(task.Name, task.About, task.TaskThemeId, task.ImportanceId,
                                        task.StartTime, task.EndTime);
                return Request.CreateResponse(HttpStatusCode.OK, "Задача успешно добавлена");
            }
            catch (AlreadyExistsException e)
            {
                //Если в базе данных уже существует тема задачи с таким именем
                return Request.CreateResponse(HttpStatusCode.Conflict, "Тема задачи с таким именем уже существует");
            }

        }


        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public HttpResponseMessage DeleteTask(int id)
        {
            //Проверяем правильность пришедшей модели
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                _taskService.DeleteTask(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Задача с id = " + id + " удалена из базы данных");
            }
            catch (TaskThemeNotExistsException e)
            {
                //Если пользователя не существует в базе данных
                return Request.CreateResponse(HttpStatusCode.NotFound,"Задачи с таким ид не существует в базе данных");
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