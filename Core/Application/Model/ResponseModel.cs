using System;
using System.Collections.Generic;
using System.Text;

namespace Blogpost.Core.Application.Model
{
    public class ResponseModel
    {
        public ResponseModel()
        {
            StatusCode = 200;
        }

        public void SetErrorMessage(string message)
        {
            Message=message ;
        }
        public string Message { get; set; }
        public object Data { get; set; }
        public int StatusCode { get; set; }
    }
}
