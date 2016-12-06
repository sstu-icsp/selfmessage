using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Exceptions.Importance
{
    public class ImportanceNotExistsException: Exception
    {
        public ImportanceNotExistsException(): base()
        {
        }

        public ImportanceNotExistsException(string message) : base(message)
        {
        }
    }
}