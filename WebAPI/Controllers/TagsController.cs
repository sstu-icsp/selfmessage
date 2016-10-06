using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Entities;

namespace WebAPI.Controllers
{
    public class TagsController : ApiController
    {

        // GET: api/Tags
        public IEnumerable<TagDTO> GetTags()
        {
            IQueryable notes = db.Notes.Include(p => p.User).Include(p => p.Tags).Where(p=>p.User.UserName==User.Identity.Name);
            List<TagDTO> tags = new List<TagDTO>();
            List<Tag> tagList = new List<Tag>();

            foreach (Note note in notes) {
                tagList.AddRange(note.Tags);
            }

            tagList = tagList.Distinct().ToList();

            foreach (Tag tag in tagList)
                {
                    TagDTO tempTag = new TagDTO();
                    tempTag.Id = tag.Id;
                    tempTag.Name = tag.Name;
                    tags.Add(tempTag);
                }
            return tags;
        }

        // GET: api/Tags/5
        [ResponseType(typeof(Tag))]
        public IHttpActionResult GetTagById(int id)
        {

            List<TagDTO> tags = new List<TagDTO>();

            //Не работает
            //foreach (Tag tag in db.Tags.Where(p => p.User.UserName == User.Identity.Name))
            foreach (Tag tag in db.Tags)
            {
                if (tag.Id == id)
                {
                    return Ok(tag);
                }
            }
            return NotFound();
        }


        private Model db = new Model();




        // PUT: api/Tags/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTag(int id, Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tag.Id)
            {
                return BadRequest();
            }

            db.Entry(tag).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
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

        // POST: api/Tags
        //[ResponseType(typeof(Tag))]
        //public IHttpActionResult PostTag(Tag tag)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Tags.Add(tag);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = tag.Id }, tag);
        //}

        // DELETE: api/Tags/5
        //[ResponseType(typeof(Tag))]
        //public IHttpActionResult DeleteTag(int id)
        //{
        //    Tag tag = db.Tags.Find(id);
        //    if (tag == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Tags.Remove(tag);
        //    db.SaveChanges();

        //    return Ok(tag);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool TagExists(int id)
        {
            return db.Tags.Count(e => e.Id == id) > 0;
        }
    }
}