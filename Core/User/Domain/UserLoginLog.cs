using Blogpost.Core.Application.Domain;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogpost.Core.User.Domain
{
    public class UserLoginLog : DomainBase
    {
        public long UserId { get; set; }
        public DateTime LoginDateTime { get; set; }
        public DateTime? LogoutDateTime { get; set; }
        public string SessionId { get; set; }

        [ForeignKey("UserId")]
        public virtual Person Person { get; set; }
    }
}
