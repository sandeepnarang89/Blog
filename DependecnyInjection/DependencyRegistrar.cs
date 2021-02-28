using System;
using Blogpost.Application.Attributes;
using System.Linq;
using Blogpost.Core.Application;
using Blogpost.Infrasturcture.Application.Impl;
using Blogpost.Infrasturcture.ORM;
using Microsoft.Extensions.DependencyInjection;

namespace Blogpost.DependecnyInjection
{
    public static class DependencyRegistrar
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            RegisterDefaultImplementations(services);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<ApplicationDbContext>(ServiceLifetime.Scoped);
        }

        private static void RegisterDefaultImplementations(IServiceCollection services)
        {
            var publicTypes = typeof(DependencyRegistrar).Assembly.GetReferencedAssemblies()
                .Where(m =>
                {
                    return m.Name.Contains("Blogpost");
                }).Select(System.Reflection.Assembly.Load).SelectMany(a => a.GetTypes());

            var interfaces = publicTypes.Where(t => t.IsInterface);
            var defaultImplementations = publicTypes.Where(type => Attribute.IsDefined(type, typeof(DefaultImplementationAttribute)));

            foreach (var type in defaultImplementations)
            {
                var attribute = (DefaultImplementationAttribute)type.GetCustomAttributes(typeof(DefaultImplementationAttribute), false).Single();
                Type typeClosure = type;

                attribute.Interface = attribute.Interface ?? interfaces.Single(i => i.Name == "I" + typeClosure.Name);
                services.AddTransient(attribute.Interface, type);
            }
        }
    }

}
