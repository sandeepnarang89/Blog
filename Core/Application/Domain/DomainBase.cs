using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogpost.Core.Application.Domain
{
    public abstract class DomainBase
    {
        public virtual long Id { get; set; }

        [NotMapped]
        public bool IsNew { get; set; }

        public virtual object[] GetId()
        {
            return new Object[] { Id };
        }
    }
}
