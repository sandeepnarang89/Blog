
namespace Blogpost.Core.Application
{
    public interface IDependencyProvider
    {
        T GetInstance<T>();
    }
}
