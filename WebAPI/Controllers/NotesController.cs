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


        //Вывод всех записей пользователя
        //api/notes


        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetNotes()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new NoteWorker(_db, User).GetAllNoteDtoOfUser());
        }

        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage GetNote(int id)
        {
            NoteWorker noteWorker = new NoteWorker(_db, User);
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, NoteWorker.ConvertFromNoteInNoteDto((noteWorker.getNoteById(id))));
            }catch (NoteNotExistsException e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
            }
        }

        //Вывод всех записей пользователя по тэгу
        //api/notes/bytag?tagName=name
        //Вместо name вставляется имя тэга
        [Route("bytag")]
        [HttpGet]
        public HttpResponseMessage GetNotesByTag(string tagName)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new NoteWorker(_db, User).GetAllNoteDtoOfUserByTag(tagName));
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
        public HttpResponseMessage Add(AddNoteDTO note)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Произошла ошибка");
            }

            new NoteWorker(_db, User).AddNote(note);

            return Request.CreateResponse(HttpStatusCode.Created, "Запись создана");
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
        public HttpResponseMessage GetNoteByName(string noteName)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new NoteWorker(_db, User).GetNoteDtoByName(noteName));
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