using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;
using WebAPI.Models.Entities;

//Пока не рабочий класс, в будущем планируется как набор инструметов
namespace WebAPI.Util
{ 
    public class Tools
    {
        private Model db;
        private string userName;

        public Tools(Model db, string userName)
        {
            this.db = db;
            this.userName = userName;
        }

        public AspNetUsers UserFind()
        {
            for (int i = 0; i < db.AspNetUsers.ToList().Count; i++)
            {
                db.SaveChanges();
                if (db.AspNetUsers.ToList()[i].UserName.Equals(userName))
                    return db.AspNetUsers.ToList()[i];
            }
            AspNetUsers newUsers = new AspNetUsers();
            db.SaveChanges();
            return newUsers;
        }

        public Tag TagFind(string name)
        {
            foreach (Tag tag in db.Tags)
            {
                if (tag.Name == name)
                {
                    return tag;
                }
            }

            Tag newTag = new Tag { Name = name };
            db.SaveChanges();
            return newTag;
        }

        public List<Tag> TagSplit(string tagString)
        {
            string[] tags = tagString.Split(' ');
            tags = tags.Distinct().ToArray();
            List<Tag> tagList = new List<Tag>();
            foreach (string tag in tags)
            {
                tagList.Add(TagFind(tag));
            }
            return tagList;
        }
    }
}