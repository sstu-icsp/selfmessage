using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Exceptions;
using WebAPI.Models.DTO;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/tasktheme")]
    public class TaskThemesController : ApiController
    {
        private TaskThemeService _taskThemService = new TaskThemeService();

        //uri api/taskthem/
        [HttpGet]
        [Route]
        public HttpResponseMessage GetAllTaskThemes()
        {
            _taskThemService = new TaskThemeService();

            var result = _taskThemService.GetAllTaskThemes();

            if (!result.Any())
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //uri api/taskthem/{id}
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetTaskTheme(int id)
        {
            try
            {
                //Получаем тему задачи
                var taskTheme = _taskThemService.GetTaskTheme(id);

                return Request.CreateResponse(HttpStatusCode.OK, taskTheme);
            }
            catch (TaskThemeNotExistsException e)
            {
                //Если тема задачи не найдена
                return Request.CreateResponse(HttpStatusCode.NotFound, e.Message);
            }
        }


        //uri api/taskthem/
        //тело: Name
        [HttpPost]
        [Route]
        public HttpResponseMessage CreateTaskTheme(TaskThemeBindingModels taskTheme)
        {
            //Проверяем указал ли пользователь тело запроса
            if (taskTheme == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Пустое тело запроса");
            }
            //Проверяем правильность пришедшей модели
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            _taskThemService = new TaskThemeService();

            try
            {
                _taskThemService.CreateTaskTheme(taskTheme.Name);
                return Request.CreateResponse(HttpStatusCode.OK, "Тема задачи успешно добавлена");
            }
            catch (AlreadyExistsException)
            {
                //Если в базе данных уже существует тема задачи с таким именем
                return Request.CreateResponse(HttpStatusCode.Conflict, "Тема задачи с таким именем уже существует");
            }
        }


        //uri api/taskthem/
        //тело: Name
        [HttpPut]
        [Route("{id}")]
        public HttpResponseMessage UpdateTaskTheme(int id, TaskThemeBindingModels taskTheme)
        {
            try
            {
                if (taskTheme == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Пустое тело запроса");
                }

                _taskThemService.UpdateTaskTheme(id, taskTheme);

                return Request.CreateResponse(HttpStatusCode.OK, "Тема задачи с id = " + id + " изменена.");
            }
            catch (AlreadyExistsException e)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, e.Message);
            }
            catch (TaskThemeNotExistsException e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, e.Message);
            }
        }

        //uri api/taskthem/{id}
        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeleteTaskTheme(int id)
        {
            try
            {
                _taskThemService.DeleteTaskTheme(id);

                return Request.CreateResponse(HttpStatusCode.OK, "Тема задач и с id = " + id + " удалена из базы данных");
            }
            catch (TaskThemeNotExistsException)
            {
                //Если пользователя не существует в базе данных
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    "Тема задачи с таким ид не существует в базе данных");
            }
        }
    }
}