using Blogpost.Application.Attributes;
using Blogpost.Core.Application;
using System.Linq;
using System.Collections.Generic;
using Blogpost.Core.Blogs.Model;
using Blogpost.Core.Blogs.Domain;

namespace Blogpost.Core.Blogs.Impl
{
    [DefaultImplementation]
    public class BlogService : IBlogService
    {
        private readonly IRepository<Blog> _blogRepository;
        public BlogService(IUnitOfWork _unitOfWork)
        {
            _blogRepository = _unitOfWork.Repository<Blog>();
        }  
    

        public bool SaveBlog(BlogEditModel model,long userId)
        {
            if (model.Id > 0)
            {
              var _blog =  _blogRepository.Table.FirstOrDefault(x => x.Id == model.Id);
                if(_blog!= default)
                {
                    _blog.Title = model.Title;
                    _blog.Content = model.Content;
                    _blogRepository.Save(_blog);
                    return true;
                }
            }
            return CreateNewBlog(model, userId);
        }

        private bool CreateNewBlog(BlogEditModel model, long userId)
        {
            _blogRepository.Save(new Domain.Blog { UserId = userId, Title = model.Title, Content = model.Content, IsNew = true });
            return true;
        }


        public BlogEditModel GetBlog(long blogId,long userId)
        {
            var _blog = _blogRepository.Table.FirstOrDefault(x => x.Id == blogId && x.UserId == userId);
            if (_blog != default)
                return new BlogEditModel { Id = _blog.Id, Title = _blog.Title, Content = _blog.Content };
            else return default;
        }

        public List<BlogEditModel> GetBlogList(long userId)
        {
            return _blogRepository.Table.Where(x => x.UserId == userId).Select(x=> new BlogEditModel { Id= x.Id, Title = x.Title, Content = x.Content}).ToList();
            
        }

        public List<BlogEditModel> List()
        {
            return _blogRepository.Table.Select(x => new BlogEditModel { Title = x.Title, Content = x.Content }).ToList();
        }

        public bool Delete(long blogId,long userId)
        {
            _blogRepository.Delete(blogId);
            return true;
        }

    }
}
