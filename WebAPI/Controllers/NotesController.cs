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
using WebAPI.Models.Test;

namespace WebAPI.Controllers
{
    [Authorize]
    public class NotesController : ApiController
    {
        private Model db = new Model();
        

        // GET: api/Notes
        public IEnumerable<User> GetNotes()
        {

            //Используются DTO объекты data transfer object
            //Если передовать обычные объекты, которые соеденены через include выдает ошибку сериализации
            //DTO объекты в данном случае своебразные представления

            List<User> users = new List<User>();

            foreach (AspNetUsers user in db.AspNetUsers)
            {
                User userTemp = new User();
                userTemp.Id = user.Id;
                userTemp.Email = user.Email;

                
                 users.Add(userTemp);
            }

            return users;
        }

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
        }

        // POST: api/Notes
        [ResponseType(typeof(Note))]
        public IHttpActionResult PostNote(Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Notes.Add(note);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = note.Id }, note);
        }

        // DELETE: api/Notes/5
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