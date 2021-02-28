using Blogpost.Application.Attributes;
using Blogpost.Core.Application;
using Blogpost.Core.User.Domain;
using System;

namespace Blogpost.Core.User.Impl
{
    [DefaultImplementation]
    public class UserLogService : IUserLogService
    {
        private readonly IRepository<UserLoginLog> _userLogRepository;
        public UserLogService(IUnitOfWork unitOfWork)
        {
            _userLogRepository = unitOfWork.Repository<UserLoginLog>();
        }


        public void SaveLoginSession(long userId, string sessionId, 
            DateTime loginDateTime )
        {
            var domain = new UserLoginLog
            {
                LoginDateTime = loginDateTime,
                SessionId = sessionId,
                UserId = userId,
                IsNew = true
            };

            _userLogRepository.Save(domain);
        }

        public void EndLoggedinSession(string token)
        {
            var log = _userLogRepository.Get(t => token.Contains( t.SessionId ) && t.LogoutDateTime == null);

            if (log == null) return;
            log.LogoutDateTime = DateTime.UtcNow;

            _userLogRepository.Save(log);
        }

    }
}
