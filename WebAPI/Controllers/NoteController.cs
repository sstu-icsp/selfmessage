using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using WebAPI.Models.Entities;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class NoteController : ApiController
    {
        
        public IEnumerable<Note> GetNote()
        {
            return new DatabaseContext().Notes.ToList<Note>();

        }
    }
}
