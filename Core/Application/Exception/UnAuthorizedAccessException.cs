using System;
using System.Collections.Generic;
using System.Text;

namespace Blogpost.Core.Application.Exception
{
    public class UnAuthorizedAccessException : BaseException
    {
        static string message = "Unauthorized access.";
        public UnAuthorizedAccessException() : base(message)
        {

        }

        public UnAuthorizedAccessException(string message)
            : base(message)
        {

        }
    }
}
