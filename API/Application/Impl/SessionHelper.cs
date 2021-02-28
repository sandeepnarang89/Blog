using Blogpost.Core.Application;
using Blogpost.Core.User;
using Blogpost.Core.User.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogpost.API.Application.Impl
{
    public static class SessionHelper
    {
        public const string ApplicationSessionCookieName = "Token";



        public static string CreateNewSession(HttpContext context, Person userLogin, IDependencyProvider dependencyProvider, string token = null)
        {
            dependencyProvider.GetInstance<IUserLogService>().SaveLoginSession(userLogin.Id, token,  DateTime.UtcNow);
            dependencyProvider.GetInstance<ISessionFactory>().BuildSession(dependencyProvider.GetInstance<ISessionContext>(), userLogin.Id);
            return token;
        }

    }
}
