using Blogpost.Application.Attributes;
using Blogpost.Core.User.Domain;
using Blogpost.Core.User.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blogpost.Core.Application.Impl
{
    [DefaultImplementation]
    public class SessionFactory : ISessionFactory
    {
        private readonly IRepository<Person> _personRepository;
        private readonly ISettings _settings;
        private readonly IRepository<UserLoginLog> _userLogRepository;
        //private readonly IRepository<InviteVendor> _inviteVendorRepository;

        public SessionFactory(IUnitOfWork unitOfWork, ISettings settings)
        {
            _personRepository = unitOfWork.Repository<Person>();
            _userLogRepository = unitOfWork.Repository<UserLoginLog>();
            _settings = settings;
        }



        public ISessionContext BuildSession(ISessionContext sessionContext, long userId)
        {
            return BuildSession(sessionContext, _personRepository.Get(userId));
        }

        public UserSessionModel GetUserSessionModel(long userId)
        {
            return BuildUserSessionModel(_personRepository.Get(userId));
        }

        public UserSessionModel GetActiveSessionModel(string sessionId)
        {
            var log = _userLogRepository.Get(t => sessionId.Contains( t.SessionId)&& t.LogoutDateTime == null);

            if (log == null)
            {
                return null;
            }
            var model = GetUserSessionModel(log.UserId);
            if (model != null)
            {
                model.LogId = log.Id;

            }
            return model;
        }

        private UserSessionModel BuildUserSessionModel(Person person)
        {
            var userLogin = _personRepository.Table.FirstOrDefault(x => x.Id == person.Id);

            if (userLogin == default)
                return default;


            var userSession = new UserSessionModel
            {
                UserId = userLogin.Id,
                UserName = userLogin.Email,
            };
            return userSession;
        }

        private ISessionContext BuildSession(ISessionContext sessionContext, Person person)
        {
            sessionContext.UserSession = BuildUserSessionModel(person);

            return sessionContext;
        }
    }
}
