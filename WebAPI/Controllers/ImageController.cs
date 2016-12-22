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
        //Сделать получение картинок записи 
        //Добавить в роут 
        //GET: api/notes/{noteId}/images
        [HttpGet]
        [Route("api/images")]
        public HttpResponseMessage GetImages()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _imageService.GetImages(Request.RequestUri.AbsoluteUri));
        }

        [HttpGet]
        [Route("api/notes/{noteId}/images")]
        public HttpResponseMessage GetImages(int noteId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _imageService.GetImages(noteId));
        }

        //Добавление картинки
        //Сделать понятное название
        //Например addimage
        //Сделать роут в котором учитывается запись, чтобы картинка привязывалась к записи
        [HttpPost]
        [Route("api/notes/{noteId}/images")]
        public HttpResponseMessage AddImage(int noteId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var file = HttpContext.Current.Request.Files[0];
            _imageService.PostImage(noteId,file.InputStream, file.ContentLength);

            return Request.CreateResponse(HttpStatusCode.Created);
        }


        //удаление картинок
        //
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