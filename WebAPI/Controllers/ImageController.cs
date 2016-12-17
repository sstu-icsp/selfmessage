using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/image")]
    public class ImagesController : ApiController
    {
        ImageService _imageService = new ImageService();

        // GET: api/image/{id}
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetImage(int id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(_imageService.StreamById(id));
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
            return result;
        }

        //GET: api/image/
        [HttpGet]
        [Route("")]
        public HttpResponseMessage getImages()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _imageService.GetImages(Request.RequestUri.AbsoluteUri));
        }

        [HttpPost]
        public HttpResponseMessage Post()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var file = HttpContext.Current.Request.Files[0];
            _imageService.PostImage(file.InputStream, file.ContentLength);

            return Request.CreateResponse(HttpStatusCode.Created);
        }

    }
}