using Blogpost.API.Application.Impl;
using Blogpost.API.Application.Model;
using Blogpost.Core.Application;
using Blogpost.Core.User;
using Blogpost.Core.User.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Blogpost.API.Area.Application
{
    [Route("api/Auth")]
    public class LoginController : ApiControllerBase
    {
        private readonly ILoginAuthenticationModelValidator _loginAuthenticationModelValidator;
        private readonly IUserLoginService _userLoginService;
        private readonly IUserLogService _userLogService;
        private readonly IDependencyProvider _dependencyProvider;
        private readonly ISettings _settings;
        private readonly ISessionContext _sessionContext;

        public LoginController(ILoginAuthenticationModelValidator loginAuthenticationModelValidator,IUserLoginService userLoginService, IDependencyProvider dependencyProvider,ISettings settings, ISessionContext sessionContext, IUserLogService userLogService) {
            _loginAuthenticationModelValidator = loginAuthenticationModelValidator;
            _userLoginService = userLoginService;
            _dependencyProvider = dependencyProvider;
            _sessionContext = sessionContext;
            _userLogService = userLogService;
            _settings = settings;

        }


        [HttpGet("check")]
        public string Check()
        {
            
            return "I'm Fine";
        }



        [HttpGet("logout")]
        public bool Logout()
        {
            Response.Cookies.Delete("Token");
            if (_sessionContext == null || _sessionContext.UserSession == null)
            {
                return true;
            }
            _userLogService.EndLoggedinSession( _sessionContext.SessionId);
            return true;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public LoginViewModel Validate([FromBody] LoginAuthenticationModel model)
        {
            (bool IsSucceeded, string Error) = _loginAuthenticationModelValidator.IsValid(model);

            if (!IsSucceeded)
            {
                return new LoginViewModel { Message = Error, Token = null };
            }
            var sessionId = Guid.NewGuid().ToString();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, model.Username),
                new Claim(ClaimTypes.Role, ("Author")),
                new Claim(JwtRegisteredClaimNames.Jti, sessionId)
            };
            var token = BuildJwtToken(claims, _settings);
             SessionHelper.CreateNewSession(HttpContext, _userLoginService.GetbyUserName(model.Username), _dependencyProvider,token);
            
            return new LoginViewModel { Message = null, Token = token };
        }

        [AllowAnonymous]

        [HttpPost("SignUp")]
        public bool SignUp([FromBody] SignUpModel model)
        {
           var response = _userLoginService.SignUp(model);
            if (response)
            {
                ResponseModel.Message = "Signup Succesfully";
            }
            return response;
        }

    }
}
