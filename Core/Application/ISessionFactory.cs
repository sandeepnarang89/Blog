using Blogpost.Core.User.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogpost.Core.Application
{
    public interface ISessionFactory
    {
        ISessionContext BuildSession(ISessionContext sessionContext, long userId);
        UserSessionModel GetUserSessionModel(long userId);
        UserSessionModel GetActiveSessionModel(string sessionId);
    }
}
