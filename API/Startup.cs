using Blogpost.API.Application.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Blogpost.DependecnyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Blogpost.Core.Application;
using Blogpost.API.Application.Filters;
using Blogpost.Infrasturcture.ORM;
using Blogpost.Infrasturcture;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDependencyProvider>(x =>
            {
                var d = new DependencyProvider
                {
                    ServiceProvider = x
                };
                return d;
            });

            services.AddHttpClient();
            services.AddScoped<ISessionContext, SessionContext>();

            DependencyRegistrar.RegisterDependencies(services);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(configureOptions =>
            {
                configureOptions.RequireHttpsMetadata = false; // 
                configureOptions.SaveToken = true; // after sucesfull authentication, token to be save on the server.

                configureOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.Default.UserSecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                configureOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                            context.Response.Headers.Add("Unauthorized AccessException", "true");
                        return System.Threading.Tasks.Task.CompletedTask;
                    }
                };
            });


            services.AddControllers();
            services.AddCors(setup => setup.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            }));



            services.AddMvc((opt) =>
            {
                opt.Filters.Add(new TransactionFilter());
                opt.Filters.Add(new DataFilter());

                opt.Filters.Add(new ExceptionFilter());
            });
            services.AddDbContext<ApplicationDbContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            var settings = app.ApplicationServices.GetRequiredService<ISettings>();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            DbStartup.InitDb(settings);
        }
    }
}
