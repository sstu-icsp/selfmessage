using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Workers;
using WebAPI.Models.Entities;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/notes")]
    [Authorize]
    public class NotesController : ApiController
    {
        //Модель базы данных
        private readonly ModelDB _db = new ModelDB();
        ImageService _imageService = new ImageService();


        //Вывод всех записей пользователя
        //api/notes


        [Route("")]
        [HttpGet]
        public IEnumerable<NoteDTO> GetNotes()
        {
            return new NoteWorker(_db, User).GetAllNoteDtoOfUser();
        }

        //Вывод всех записей пользователя по тэгу
        //api/notes/bytag?tagName=name
        //Вместо name вставляется имя тэга
        [Route("bytag")]
        [HttpGet]
        public IEnumerable<NoteDTO> GetNotesByTag(string tagName)
        {
            return new NoteWorker(_db, User).GetAllNoteDtoOfUserByTag(tagName);
        }


        //Добавления записи пользователю
        //url api/notes/
        //post
        //data -----
        //Name - строка с именем
        //Text - строка с текстом записи
        //Tags - строка с тэгам. Тэги разделяются или пробелом или #
        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(AddNoteDTO note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Image image = null;

            if (HttpContext.Current.Request.Files.Count > 1)
            {
                var file = HttpContext.Current.Request.Files[0];
                image = _imageService.PostImage(file.InputStream, file.ContentLength);
            }

            new NoteWorker(_db, User).AddNote(note, image);

            return Ok();
        }

        //url api/notes/{id}
        //тело: Name
        [HttpPut]
        [Route("{id}")]
        public HttpResponseMessage UpdateNote(int id, AddNoteDTO note)
        {
            try
            {
                if (note == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Пустое тело запроса");
                }

                new NoteWorker(_db, User).UpdateNote(id, note);

                return Request.CreateResponse(HttpStatusCode.OK, "Запись изменена.");
            }
            catch (AlreadyExistsException e)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, e.Message);
            }
            catch (NoteNotExistsException e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, e.Message);
            }
        }

        //Уделанеи записи по id
        //api/notes/{id}
        [Route("{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            if (new NoteWorker(_db, User).getNoteById(id) == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Запись не найдена");

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Невозможно удалить запись");
            }

            new NoteWorker(_db, User).DeleteNote(id);

            return Request.CreateResponse(HttpStatusCode.OK, "Запись удалена");
        }


        //Получение тэга по имени
        //api/notes/byname?notename=name
        //name заменяется на название тэга
        [Route("byname")]
        [HttpGet]
        public IEnumerable<NoteDTO> GetNoteByName(string noteName)
        {
            return new NoteWorker(_db, User).GetNoteDtoByName(noteName);
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