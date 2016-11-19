using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Entities;
using WebAPI.Workers.TagSpliters;

namespace WebAPI.Workers
{
    public class NoteWorker
    {
        private readonly ModelDB _db;
        private readonly IPrincipal _user;


        public NoteWorker(ModelDB db, IPrincipal user)
        {
            _db = db;
            _user = user;
        }

        //Метод для получения всех записей пользователя
        public IEnumerable<NoteDTO> GetAllNoteDtoOfUser()
        {
            return ConvertFromNoteInNoteDto(AllNotes);
        }

        //Метод для получения всех записей пользователя в которых содержиться строка noteName в имени записи
        public IEnumerable<NoteDTO> GetNoteDtoByName(string noteName)
        {
            var notes = _db.Notes.Include(p=>p.Tags)
                    .Where(p => p.Name.Contains(noteName));
            return ConvertFromNoteInNoteDto(notes);
        }

        //Метод для получения всех dto тэгов пользователя
        public IEnumerable<NoteDTO> GetAllNoteDtoOfUserByTag(string tagName)
        {
            return ConvertFromNoteInNoteDto(GetAllNotesByTag(tagName));
        }

        //Метод для добавления записи в базу данных
        public void AddNote(AddNoteDTO addNoteDto)
        {
            _db.Notes.Add(new Note
            {
                Name = addNoteDto.Name,
                Text = addNoteDto.Text,
                Tags = TagWorker.GetTagsFromString(addNoteDto.Tags, _db),
                User = UserWorker.GetUserByName(_user.Identity.Name, _db),
                DataAdded = DateTime.Now
            });
            _db.SaveChanges();
        }

        //Метод переводящий список объектов note в notedto
        private static IEnumerable<NoteDTO> ConvertFromNoteInNoteDto(IEnumerable<Note> notes)
        {
            ICollection<NoteDTO> dtoNotes = new List<NoteDTO>();

            foreach (var note in notes)
            {
                var dtoNote = new NoteDTO
                {
                    Id = note.Id,
                    Name = note.Name,
                    Text = note.Text,
                    DateAdded = note.DataAdded
                };

                foreach (var tag in note.Tags)
                {
                    var tempTag = new TagDTO
                    {
                        Id = tag.Id,
                        Name = tag.Name
                    };
                    dtoNote.Tags.Add(tempTag);
                }

                dtoNotes.Add(dtoNote);
            }

            return dtoNotes;
        }

        //Метод для получения всех записей пользователя 
        private IEnumerable<Note> AllNotes
        {
            get
            {
                return _db.Notes.Include(p => p.User).Include(p => p.Tags)
                       .Where(p => p.User.UserName == _user.Identity.Name);
            }
        }
        //Метод для получения всех записей пользователя по названию тэга
        private IEnumerable<Note> GetAllNotesByTag(string tagName)
        {
            return AllNotes.Where(note => note.Tags
            .Any(tag => tag.Name.Equals(tagName)))
            .ToList();
        }
    }
}