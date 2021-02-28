using System;
using System.Collections.Generic;
using System.Text;

namespace Blogpost.DependecnyInjection
{
    public class DependencyProvider : Core.Application.IDependencyProvider
    {
        public IServiceProvider ServiceProvider { get; set; }

        public T GetInstance<T>()
        {
            return (T)ServiceProvider.GetService(typeof(T));
        }
    }
}
