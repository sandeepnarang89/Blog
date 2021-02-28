
namespace Blogpost.Core.Application
{
    public interface ISettings
    {
        string ConnectionString { get; }
        string BaseUrl { get; }
        string UserSecretKey { get; }

    }
}
