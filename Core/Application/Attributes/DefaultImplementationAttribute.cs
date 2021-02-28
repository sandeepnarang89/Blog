using System;
using System.Collections.Generic;
using System.Text;

namespace Blogpost.Application.Attributes
{
    public class DefaultImplementationAttribute : Attribute
    {
        public Type Interface { get; set; }

        public string RegistrationName { get; set; }

        public DefaultImplementationAttribute()
        {
        }

    }
}
