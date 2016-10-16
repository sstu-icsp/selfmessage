using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Entities;
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

        [Route("bytag")]
        [HttpGet]
        public IEnumerable<NoteDTO> GetNotesForTag(GetNotesForTagDTO tagDTO)
        {

            //Используются DTO объекты data transfer object
            //Если передовать обычные объекты, которые соеденены через include выдает ошибку сериализации
            //DTO объекты в данном случае своебразные представления

            List<NoteDTO> allNotes=GetNotes().ToList();
            List<NoteDTO> endNotes = new List<NoteDTO>();

            foreach(NoteDTO note in allNotes)
            {
                foreach(TagDTO tag in note.Tags)
                {
                    if(tag.Name==tagDTO.Name)
                    {
                        endNotes.Add(note);
                        break;
                    }
                }
            }
           

            return endNotes;
        }*/


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