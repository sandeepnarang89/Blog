using Blogpost.Infrasturcture.Application.Impl;
using Blogpost.Infrasturcture.ORM;
using Blogpost.Core.Application.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogpost.Infrasturcture.Application
{
    public class RepositoryProvider
    {
        private static Dictionary<Type, Type> repositoryDictionary = new Dictionary<Type, Type>();
        public static BaseRepository GetInstance<T>(ApplicationDbContext dbContext) where T : DomainBase
        {
            if (repositoryDictionary.ContainsKey(typeof(T)))
            {
                return (BaseRepository)Activator.CreateInstance(repositoryDictionary[typeof(T)], dbContext);
            }

            return new Repository<T>(dbContext);
        }
    }
}
