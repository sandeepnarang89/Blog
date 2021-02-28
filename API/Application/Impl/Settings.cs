using Blogpost.Application.Attributes;
using Microsoft.Extensions.Configuration;

namespace Blogpost.API.Application.Impl
{
    [DefaultImplementation]
    public class Settings: Core.Application.ISettings
    {
        public static Settings Default { get; set; }
        public Settings(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
            UserSecretKey = configuration["Settings:UserSecretKey"];
            BaseUrl = configuration["Settings:BaseUrl"];

        }
        public string ConnectionString { get; private set; }
        public string BaseUrl { get; private set; }
        public string UserSecretKey { get; private set; }

    }
}
