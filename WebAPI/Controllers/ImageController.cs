using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using WebAPI.Exceptions;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class ImagesController : ApiController
    {

        //Сервис для работы с картинками
        ImageService _imageService = new ImageService();


        //Получение картинки по ид картинки
        //Протестированно postman
        // GET: api/image/{id}
        [HttpGet]
        [Route("api/images/{id}")]
        public HttpResponseMessage GetImage(int id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(_imageService.StreamById(id));
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
            return result;
        }


        //Получение ссылок на все картинки
        //GET: api/images
        [HttpGet]
        [Route("api/images")]
        public HttpResponseMessage GetImages()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _imageService.GetImages(Request.RequestUri.AbsoluteUri));
        }


        //Получение ссылок на картинки конкретной записи
        //GET: api/notes/{noteId}/images
        [HttpGet]
        [Route("api/notes/{noteId}/images")]
        public HttpResponseMessage GetImages(int noteId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _imageService.GetImages(noteId));
        }

        //Добавление картинки к записи
        //POST: api/notes/{noteId}/images
        [HttpPost]
        [Route("api/notes/{noteId}/images")]
        public HttpResponseMessage AddImage(int noteId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var files = HttpContext.Current.Request.Files;
            _imageService.PostImage(noteId,files);

            return Request.CreateResponse(HttpStatusCode.Created);
        }


        //удаление картинок
        //DELETE: api/images/{id}
        [HttpDelete]
        [Route("api/images/{id}")]
        public HttpResponseMessage Delete(int id)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                _imageService.DeleteImage(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Картинка удалена");
            }
            catch (ImageNotExistException ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Картинка не найдена");
            }
        }

    }
}