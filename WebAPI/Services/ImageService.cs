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
        //Получение записей по URI
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

        //Получение записей по Id записи
        public IEnumerable<String> GetImages(int noteId)
        {
            using (ModelDB db = new ModelDB())
            {
                List<String> strings = new List<String>();

                List<Image> images = db.Notes.Include("Images").FirstOrDefault(p=>p.Id==noteId).Images.ToList();

                foreach (Image image in images)
                {
                    strings.Add("http://localhost:9343/api/images/" + image.Id);
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

        public void PostImage(int noteId, HttpFileCollection files)
        {
            using (ModelDB db = new ModelDB())
            {
                var notes = db.Notes.Where(p => p.Id == noteId).ToList();

                for(int i = 0; i < files.Count; i++) {
                    Image image = new Image { Stream = CreateStream(files[i].InputStream, files[i].ContentLength), Notes = notes };
                    db.Images.Add(image);
                    db.SaveChanges();
                } 
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