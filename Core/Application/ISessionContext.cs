
using Blogpost.Core.User.Model;

namespace Blogpost.Core.Application
{
    public interface ISessionContext
    {
        UserSessionModel UserSession { get; set; }
        string SessionId { get; set; }
    }
}
