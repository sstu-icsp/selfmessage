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
        private readonly Model _db = new Model();


        //Вывод всех записей
        [Route("")]
        [HttpGet]
        public IEnumerable<NoteDTO> GetNotes()
        {
            return new NoteWorker(_db, User).GetAllNoteDtoOfUser();
        }

        [Route("bytag")]
        [HttpGet]
        public IEnumerable<NoteDTO> GetNotesByTag(string tagName)
        {
            return new NoteWorker(_db, User).GetAllNoteDtoOfUserByTag(tagName);
        }

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