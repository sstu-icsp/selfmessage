using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Models.Entities;

namespace WebAPI.Services
{
    public class ImageService
    {

        public IEnumerable<String> GetImages(string requestURI)
        {
            using (ModelDB db = new ModelDB())
            {
                List<String> strings = new List<String>();

                foreach (Image image in db.Images)
                {
                    strings.Add(requestURI + "/" + image.Id);
                }

                return strings;
            }
        }

        public IEnumerable<String> GetImages(IEnumerable<Image> images)
        {
            using (ModelDB db = new ModelDB())
            {
                List<String> strings = new List<String>();

                foreach (Image image in images)
                {
                    strings.Add("http://localhost:9343/api/image/" + image.Id);
                }

                return strings;
            }
        }

        public byte[] StreamById(int id)
        {
            using (ModelDB db = new ModelDB())
            {
                return db.Images.FirstOrDefault(p => p.Id == id).Stream;
            }
        }

        public Image PostImage(Stream stream, int contentLength)
        {
            using (ModelDB db = new ModelDB())
            {
                Image image = new Image { Stream = CreateStream(stream, contentLength) };

                db.Images.Add(image);
                db.SaveChanges();

                return image;
            }
        }

        public void DeleteImage(int id)
        {
            //Открываем соеденение с базой данных
            var db = new ModelDB();

            try
            {
                var imageForRemove = db.Images.Find(id);

                if (imageForRemove == null)
                {
                    throw new ImageNotExistException("Картинка с таким ид не существует");
                }

                db.Images.Remove(imageForRemove);

                db.SaveChanges();
            }
            finally
            {
                db.Dispose();
            }
        }

        private byte[] CreateStream(Stream stream, int contentLength)
        {
            byte[] imageData;

            using (var binaryReader = new BinaryReader(stream))
            {
                imageData = binaryReader.ReadBytes(contentLength);
            }

            return imageData;
        }
    }
}