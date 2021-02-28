using System;
using System.Collections.Generic;
using System.Text;

namespace Blogpost.Core.Application.Model
{
    public class ModelValidationOutput
    {
        public ModelValidationOutput()
        {
            IsValid = true;
        }
        public bool IsValid { get; set; } = true;
        public List<ModelValidationItem> Errors { get; set; }
    }
}
