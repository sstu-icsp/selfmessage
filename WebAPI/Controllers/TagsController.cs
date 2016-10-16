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
using WebAPI.Workers;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/tags")]
    [Authorize]
    public class TagsController : ApiController
    {
        private readonly Model _db = new Model();


        [Route("")]
        [HttpGet]
        public IEnumerable<TagDTO> GetTags()
        {
            return new TagWorker(_db, User).GetAllTagDtoOfUser();
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