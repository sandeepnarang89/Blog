using Blogpost.Core.Blogs.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogpost.Core.Blogs
{
    public interface IBlogService
    {
        bool SaveBlog(BlogEditModel model, long userId);
        BlogEditModel GetBlog(long blogId, long userId);
        List<BlogEditModel> GetBlogList(long userId);
        List<BlogEditModel> List();
        bool Delete(long blogId, long userId);
    }
}
