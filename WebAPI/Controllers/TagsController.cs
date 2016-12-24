using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Workers;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/tags")]
    [Authorize]
    public class TagsController : ApiController
    {
        private readonly ModelDB _db = new ModelDB();

        //Получение всех тэгов пользователя
        //api/tags
        //get
        [Route("")]
        [HttpGet]
        public HttpResponseMessage GetTags()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new TagWorker(_db, User).GetAllTagDtoOfUser());
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