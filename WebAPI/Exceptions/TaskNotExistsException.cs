using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Exceptions
{
    public class TaskNotExistsException:Exception
    {
        public TaskNotExistsException()
        {
        }

        public TaskNotExistsException(string message) : base(message)
        {
        }
    }
}