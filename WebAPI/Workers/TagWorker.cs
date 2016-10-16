using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Entities;

namespace WebAPI.Workers
{
    public class TagWorker
    {
        private readonly Model _db;
        private readonly IPrincipal _user;

        public TagWorker(Model db, IPrincipal user)
        {
            _db = db;
            _user = user;
        }


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


        public static ICollection<Tag> GetTagsFromString(string tagString, Model db)
        {
            ICollection<Tag> tags = TagStringSplit(tagString)
                .Select(tagName => FindOrCreateTagByName(tagName, db)).ToList();

            return tags;
        }


        private static IEnumerable<string> TagStringSplit(string tagString)
        {
            return tagString.Split(" #".ToCharArray()).Distinct().ToList();
        }

        private static Tag FindOrCreateTagByName(string tagName, Model db)
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

        private static Tag FindTagByName(string tagName, Model db)
        {
            return db.Tags.FirstOrDefault(tag => tag.Name.Equals(tagName));
        }


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