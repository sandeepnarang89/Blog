using System;
using System.Collections.Generic;
using System.Text;

namespace Blogpost.Core.Application.Exception
{
    [Serializable]
    public class ValidationFailureException : BaseException
    {
        static string message = "Data validations failed. Please check the response data.";
        public ValidationFailureException() : base(message)
        {
        }

    }
}
