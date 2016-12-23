using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Models.DTO;
using WebAPI.Models.Entities;
using WebAPI.Services;
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
            Note note = new Note
            {
                Name = addNoteDto.Name,
                Text = addNoteDto.Text,
                Tags = TagWorker.GetTagsFromString(addNoteDto.Tags, _db),
                User = UserWorker.GetUserByName(_user.Identity.Name, _db),
                DataAdded = DateTime.Now
            };

            _db.Notes.Add(note);
            _db.SaveChanges();
        }

        //Метод для обновления записи
        public void UpdateNote(int id, AddNoteDTO addNoteDto)
        {
            var note = getNoteById(id);
            TagWorker.DeleteTagsLinkNoteId(id, _db);
            TagWorker.DeleteTagsNoLinks(_db);
            note.Name = addNoteDto.Name;
            note.Text = addNoteDto.Text;
            note.Tags = TagWorker.GetTagsFromString(addNoteDto.Tags, _db);
            note.User = UserWorker.GetUserByName(_user.Identity.Name, _db);


            _db.SaveChanges();
        }

        //Метод переводящий объект note в notedto
        public static NoteDTO ConvertFromNoteInNoteDto(Note note)
        {
            ImageService imageService = new ImageService();

            var dtoNote = new NoteDTO
            {
                Id = note.Id,
                Name = note.Name,
                Text = note.Text,
                DateAdded = note.DataAdded,
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
            return dtoNote;

        }

        //Метод переводящий список объектов note в notedto
        public static IEnumerable<NoteDTO> ConvertFromNoteInNoteDto(IEnumerable<Note> notes)
        {
            ICollection<NoteDTO> dtoNotes = new List<NoteDTO>();

            foreach (var note in notes)
            {
                //НЕ РЕКУРСИЯ вызывается ConvertFromNoteInNoteDto(note) для конвертации одной сущности
                dtoNotes.Add(ConvertFromNoteInNoteDto(note));
            }

            return dtoNotes;
        }

        //Метод удаления пользователя
        public void DeleteNote(int id)
        {
            _db.Notes.Remove(_db.Notes.Find(id));
            TagWorker.DeleteTagsNoLinks(_db);
            _db.SaveChanges();

        }

        //Получение записи по id
        public Note getNoteById(int id)
        {
            var note= _db.Notes.Include("Tags").FirstOrDefault(p => p.Id == id);

            if (note == null)
            {
                throw new NoteNotExistsException("Нет записи с таким id");
            }
            return note;
        }

        //Метод для получения всех записей пользователя 
        private IEnumerable<Note> AllNotes
        {
            get
            {
                return _db.Notes.Include(p => p.User).Include(p => p.Tags).Include(p=>p.Images)
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