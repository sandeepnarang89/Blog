using System;
using System.Collections.Generic;
using System.Text;

namespace Blogpost.Core.Application.Model
{
    public class ModelValidationItem
    {
        public string Name { get; set; }
        public string Error { get; set; }

        public ModelValidationItem(string name, string error)
        {
            Name = name;
            Error = error;
        }
    }
}
