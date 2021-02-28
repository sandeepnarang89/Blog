using System;
using System.Collections.Generic;
using System.Text;

namespace Blogpost.Core.User
{
    public interface ILoginAuthenticationModelValidator
    {
        (bool IsSucceeded, string Error) IsValid(Model.LoginAuthenticationModel model);
    }
}
