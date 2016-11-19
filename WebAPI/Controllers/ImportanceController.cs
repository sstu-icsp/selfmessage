using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Exceptions;
using WebAPI.Exceptions.Importance;
using WebAPI.Models.DTO;
using WebAPI.Models.DTO.Importance;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/importance")]
    public class ImportanceController : ApiController
    {
        private ImportanceService _importanceService = new ImportanceService();

        //uri api/taskthem/
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAllImportances()
        {
            _importanceService = new ImportanceService();

            var result = _importanceService.GetAllImportances();

            if (!result.Any())
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //uri api/taskthem/{id}
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetImportance(int id)
        {
            try
            {
                //Получаем тему задачи
                var importance = _importanceService.GetImportance(id);

                return Request.CreateResponse(HttpStatusCode.OK, importance);
            }
            catch (ImportanceNotExistsException e)
            {
                //Если тема задачи не найдена
                return Request.CreateResponse(HttpStatusCode.NotFound, e.Message);
            }
        }


        //uri api/taskthem/
        //тело: Name
        [HttpPost]
        [Route("")]
        public HttpResponseMessage CreateImportance(ImportanceBindingModel importance)
        {
            //Проверяем указал ли пользователь тело запроса
            if (importance == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Пустое тело запроса");
            }
            //Проверяем правильность пришедшей модели
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            _importanceService = new ImportanceService();

            try
            {
                _importanceService.CreateImportance(importance.Name);
                return Request.CreateResponse(HttpStatusCode.OK, "Тема задачи успешно добавлена");
            }
            catch (AlreadyExistsException e)
            {
                //Если в базе данных уже существует тема задачи с таким именем
                return Request.CreateResponse(HttpStatusCode.Conflict, "Тема задачи с таким именем уже существует");
            }
        }


        //uri api/taskthem/
        //тело: Name
        [HttpPut]
        [Route("{id}")]
        public HttpResponseMessage UpdateImportance(int id, ImportanceBindingModel importance)
        {
            try
            {
                if (importance == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Пустое тело запроса");
                }

                _importanceService.UpdateImportance(id, importance);

                return Request.CreateResponse(HttpStatusCode.OK, "Важность с id = " + id + " изменена.");
            }
            catch (AlreadyExistsException e)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, e.Message);
            }
            catch (ImportanceNotExistsException e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, e.Message);
            }
        }

        //uri api/taskthem/{id}
        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeleteImportance(int id)
        {
            try
            {
                _importanceService.DeleteImportance(id);

                return Request.CreateResponse(HttpStatusCode.OK, "Важность и с id = " + id + " удалена из базы данных");
            }
            catch (ImportanceNotExistsException)
            {
                //Если пользователя не существует в базе данных
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    "Тема задачи с таким ид не существует в базе данных");
            }
        }
    }
}