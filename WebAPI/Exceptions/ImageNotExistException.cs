using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Exceptions
{
    public class ImageNotExistException : Exception
    {
        public ImageNotExistException() : base()
        {

        }

        public ImageNotExistException(string message) : base()
        {

        }
    }
}