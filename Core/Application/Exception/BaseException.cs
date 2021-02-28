using System;
using System.Collections.Generic;
using System.Text;

namespace Blogpost.Core.Application.Exception
{
    public class BaseException : System.Exception
    {
        public BaseException(string message) : base(message)
        {
        }
        public BaseException()
        {
        }
        public BaseException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

    }
}
