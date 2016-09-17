using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class TestController : ApiController
    {
        public Note GetNote ()
        {
            return new Note { Id = 1, Name = "test note", Text = "test text of note" };
        }
    }
}
