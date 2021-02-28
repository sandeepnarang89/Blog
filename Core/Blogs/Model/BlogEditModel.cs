using System.ComponentModel.DataAnnotations;

namespace Blogpost.Core.Blogs.Model
{
    public class BlogEditModel
    {
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
