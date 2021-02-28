using Blogpost.Core.Application.Domain;
using Blogpost.Core.User.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blogpost.Core.Blogs.Domain
{
    public class Blog : DomainBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public Person Person { get; set; }
    }
}
