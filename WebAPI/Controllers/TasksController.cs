using System.Net;
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

        //PUT: api/tasks/{id}
        [HttpPut]
        [Route("{id}")]
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
        [HttpPut]
        [Route("{id}/tasktheme")]
        public HttpResponseMessage TaskThemeEdit(int id,TaskThemeEdit taskThemeEdit)
        {
            try
            {
                //Помечаем задачу как выполненную
                new TaskService().TaskThemeEdit(id, taskThemeEdit.TaskTheme);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (TaskNotExistsException e)
            {
                //Если задача не найдена
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        //DELETE: api/tasks/{id}
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