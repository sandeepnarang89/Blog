using Blogpost.Core.Application;
using Blogpost.Infrasturcture.Application.Impl;
using Blogpost.Infrasturcture.ORM;

namespace Blogpost.Infrasturcture
{
    public class DbStartup
    {
        protected DbStartup()
        {

        }
        public static void InitDb(ISettings settings)
        {
            using (var uow = new UnitOfWork(settings))
            {
                ApplicationDbContext DbContext = (ApplicationDbContext)uow.DbContext;
                DbContext.InitDb();
            }
        }
    }
}
