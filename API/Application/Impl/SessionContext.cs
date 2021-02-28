using Blogpost.Core.Application;
using Blogpost.Core.User.Model;

namespace Blogpost.API.Application.Impl
{
    public class SessionContext : ISessionContext
    {
        public UserSessionModel UserSession { get; set; }

        public string SessionId { get; set; }
    }
}
