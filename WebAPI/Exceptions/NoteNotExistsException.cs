using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Exceptions
{
    public class NoteNotExistsException: Exception
    {
        public NoteNotExistsException(): base()
        {
        }

        public NoteNotExistsException(string message) : base(message)
        {
        }
    }
}