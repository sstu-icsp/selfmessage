using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Entities;
using WebAPI.Workers.TagSpliters;

namespace WebAPI.Workers
{
    public class TagWorker
    {
        private readonly ModelDB _db;
        private readonly IPrincipal _user;

        private static ITagSplit _tagSplit = new TagSplitBySharpAndSpace();

        public TagWorker(ModelDB db, IPrincipal user)
        {
            _db = db;
            _user = user;

        }

        //Слеудующие три метода просто меняют стратегию разбиения тэгов
        public static void SetSplitTagStringBySharp()
        {
            _tagSplit = new TagSplitBySharp();
        }

        public static void SetSplitTagStringBySpace()
        {
            _tagSplit = new TagSplitBySpace();
        }

        public static void SetSplitTagStringBySharpAndSpace()
        {
            _tagSplit = new TagSplitBySharpAndSpace();
        }

        //Метод для получения всех dto объектов тэгов пользователя
        public IEnumerable<TagDTO> GetAllTagDtoOfUser()
        {
            var notes = _db.Notes.Include(p => p.Tags)
                .Where(p => p.User.UserName.Equals(_user.Identity.Name));

            var tags = new List<Tag>();

            foreach (var note in notes)
            {
                tags.AddRange(note.Tags);
            }

            tags = tags.Distinct().ToList();

            return ConvertFromTagInTagDto(tags);
        }


        //Метод возращающий все тэги из строки 
        //выделяет тэги из строки и находит их в базе данных или создает новый
        public static ICollection<Tag> GetTagsFromString(string tagString, ModelDB db)
        {
            if (!tagString.Equals(""))
            {
                ICollection<Tag> tags = _tagSplit.TagStringSplit(tagString)
                .Select(tagName => FindOrCreateTagByName(tagName, db)).ToList();
                return tags;
            }
            else return null;
        }


        //Метод удаляющий связи между записями и записью
        public static void DeleteTagsLinkNoteId(int id, ModelDB db)
        {
            var notes = db.Notes.Include(p => p.Tags)
                .Where(p => p.Id==id);

            foreach(var note in notes)
            {
                note.Tags = null;
            }
        }

        //Метод удаляющий тэги без связей с записями
        public static void DeleteTagsNoLinks(ModelDB db)
        {
            db.SaveChanges();
            foreach (var tag in db.Tags.Include(p =>p.Notes))
            {
                if (tag.Notes.Count == 0)
                    db.Tags.Remove(tag);
            }

            db.SaveChanges();

        }

        //Метод вызывающий поиск тэгов по названию и если он возврщает null 
        //создает новый тэг в противном случает отправляет найденный
        private static Tag FindOrCreateTagByName(string tagName, ModelDB db)
        {
            var tag = FindTagByName(tagName, db);

            if (tag != null) return tag;

            tag = new Tag
            {
                Name = tagName
            };
            db.Tags.Add(tag);
            db.SaveChanges();

            return tag;
        }

        //Метод для нахождения тэгов по имени возврщает null, если тэг не найден
        private static Tag FindTagByName(string tagName, ModelDB db)
        {
            return db.Tags.FirstOrDefault(tag => tag.Name.Equals(tagName));
        }

        //Метод для конфертирования объектов из tag в tagdto
        private static IEnumerable<TagDTO> ConvertFromTagInTagDto(IEnumerable<Tag> tags)
        {
            return tags.Select(tag => new TagDTO
            {
                Id = tag.Id,
                Name = tag.Name
            }).ToList();
        }
    }
}