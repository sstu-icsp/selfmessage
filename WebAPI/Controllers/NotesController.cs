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

namespace WebAPI.Controllers
{
    [Authorize]
    public class NotesController : ApiController
    {
        //Модель базы данных
        private Model db = new Model();
        

        // GET: api/Notes
        public IEnumerable<NoteDTO> GetNotes()
        {

            //Используются DTO объекты data transfer object
            //Если передовать обычные объекты, которые соеденены через include выдает ошибку сериализации
            //DTO объекты в данном случае своебразные представления

            List<NoteDTO> notes = new List<NoteDTO>();

            foreach (Note note in db.Notes.Include(p=>p.User).Include(p=>p.Tags)
                .Where(p=>p.User.UserName == User.Identity.Name))
            {
                NoteDTO tempNote = new NoteDTO();
                tempNote.Id = note.Id;
                tempNote.Name = note.Name;
                tempNote.Text = note.Text;
                tempNote.DateAdded = note.DataAdded;

                foreach(Tag tag in note.Tags)
                {
                    TagDTO tempTag = new TagDTO();
                    tempTag.Id = tag.Id;
                    tempTag.Name = tag.Name;
                    tempNote.Tags.Add(tempTag);
                }
                notes.Add(tempNote);
            }

            return notes;
        }


        //Тестовый метод, для проверки того, какой пользователь сейчас находится в систему
        [Authorize]
        [HttpGet]
        [Route("Test")]
        public string Test()
        {
            return User.Identity.Name;
        }

        /*
        // GET: api/Notes/5
        [ResponseType(typeof(Note))]
        public IHttpActionResult GetNote(int id)
        {
            Note note = db.Notes.Find(id);
            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        // PUT: api/Notes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNote(int id, Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.Id)
            {
                return BadRequest();
            }

            db.Entry(note).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }*/

        //Метод добавления записи
        [Authorize]
        [HttpPost]
        [Route("api/notes/add")]
        public IHttpActionResult Add(AddNoteDTO note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Notes.Add(new Note { Text = note.Text, Name = note.Name, DataAdded = DateTime.Today, User = UserFind(), Tags=TagSplit(note.Tags)});
            db.SaveChanges();

            return Ok();
        }

        //Метод преобразования строки тегов в полноценные объекты
        //FIXME Надо дописать сплиты и сделать проверку на повторение тега
        private List<Tag> TagSplit(string tagString)
        {
            string[] tags = tagString.Split(' ');
            tags=tags.Distinct().ToArray();
            List<Tag> tagList = new List<Tag>();
            foreach (string tag in tags)
            {
                tagList.Add(TagFind(tag));
            }
            return tagList;
        }

        //Метод для поиска пользователя, авторизированного в системе
        private AspNetUsers UserFind()
        {
            for (int i = 0; i < db.AspNetUsers.ToList().Count; i++)
            {
                if (db.AspNetUsers.ToList()[i].UserName.Equals(User.Identity.Name))
                    return db.AspNetUsers.ToList()[i];
            }
            return new AspNetUsers();
        }

        //Метод поиска тэга по имени, если тег не найден создается новый
        private Tag TagFind(string name)
        {
            foreach(Tag tag in db.Tags)
            {
                if (tag.Name == name)
                    return tag;
            }

            return new Tag { Name = name};
        }

        /* // DELETE: api/Notes/5
         [ResponseType(typeof(Note))]
         public IHttpActionResult DeleteNote(int id)
         {
             Note note = db.Notes.Find(id);
             if (note == null)
             {
                 return NotFound();
             }

             db.Notes.Remove(note);
             db.SaveChanges();

             return Ok(note);
         }*/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /*private bool NoteExists(int id)
        {
            return db.Notes.Count(e => e.Id == id) > 0;
        }*/
    }
}