using System;
using System.Collections.Generic;
using System.Text;

namespace Blogpost.Core.Application.Model
{
    public class PostResponseModel : ResponseModel
    {
        public ModelValidationOutput ModelValidation { get; set; }
    }
}
