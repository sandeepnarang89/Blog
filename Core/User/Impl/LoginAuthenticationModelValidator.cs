
using Blogpost.Application.Attributes;

namespace Blogpost.Core.User.Impl
{
    [DefaultImplementation]
    public class LoginAuthenticationModelValidator : ILoginAuthenticationModelValidator
    {
        private readonly IUserLoginService _userLoginService;
        public LoginAuthenticationModelValidator(IUserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }

        public (bool IsSucceeded, string Error) IsValid(Model.LoginAuthenticationModel model)
        {
            var userLogin = _userLoginService.GetbyUserName(model.Username);
            if (userLogin == null || !userLogin.Password.Equals(model.Password))
            {
                return (false,  "Invalid login credentials.");
            }
            return (true, null);
        }

    }
}
