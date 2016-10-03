using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Entities;
using System.Data.Entity;

namespace WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/tags")]
    public class TagsController : ApiController
    {
        private Model db = new Model();

        [HttpGet]
        [Route("")]
        public IEnumerable<TagDTO> Get()
        {
            List<TagDTO> tags = new List<TagDTO>();


            foreach(Tag tag in db.Tags.Include(p=>p.User).Where(p=>p.User.UserName.Equals(User.Identity.Name)))
            {
                TagDTO tempTag = new TagDTO();

                tempTag.Id = tag.Id;
                tempTag.Name = tag.Name;

                tags.Add(tempTag);
            }

            return tags;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
