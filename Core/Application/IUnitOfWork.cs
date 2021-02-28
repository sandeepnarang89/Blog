using Blogpost.Core.Application.Domain;

namespace Blogpost.Core.Application
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : DomainBase;
        void StartTransaction();
        void BeginWork(ISettings settings, IDependencyProvider dependencyProvider);
        void Commit();
        void Rollback();
        void Cleanup();
    }
}
