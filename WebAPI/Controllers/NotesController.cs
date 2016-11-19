using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Workers;

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
        //api/notes/add
        //post
        //data -----
        //Name - строка с именем
        //Text - строка с текстом записи
        //Tags - строка с тэгам. Тэги разделяются или пробелом или #
        [Route("add")]
        [HttpPost]
        public IHttpActionResult Add(AddNoteDTO note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            new NoteWorker(_db, User).AddNote(note);

            return Ok();
        }


        //Получение тэга по имени
        //api/notes/byname?notename=name
        //name заменяется на название тэга
        [Route("byname")]
        [HttpGet]
        public IEnumerable<NoteDTO> GetNoteByName(string noteName)
        {
            return new NoteWorker(_db,User).GetNoteDtoByName(noteName);
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