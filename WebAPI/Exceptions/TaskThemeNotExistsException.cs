using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Exceptions
{
    public class TaskThemeNotExistsException : Exception
    {
        public TaskThemeNotExistsException()
        {
        }

        public TaskThemeNotExistsException(string message) : base(message)
        {
        }
    }
}