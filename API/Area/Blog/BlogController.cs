using Blogpost.API.Area.Application;
using Blogpost.Core.Application;
using Blogpost.Core.Blogs;
using Blogpost.Core.Blogs.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Blogpost.API.Area
{
    [Route("api/[controller]")]
    public class BlogController : ApiControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly ISessionContext _sessionContext;

        public BlogController(IBlogService blogService,ISessionContext sessionContext)
        {
            _blogService = blogService;
            _sessionContext = sessionContext;
        }

        [HttpPost("save")]
        public bool Save([FromBody] BlogEditModel model)
        {
            var response = _blogService.SaveBlog(model, _sessionContext.UserSession.UserId);
            if (response) ResponseModel.Message = "Blog Save Succeffully.";
            return response;
        }

        [HttpGet("get/{blogId}")]
        public BlogEditModel Get(long blogId)
        {
            var response = _blogService.GetBlog(blogId, _sessionContext.UserSession.UserId);
            if (response == null) ResponseModel.Message = "No Blog found for you.";
            return response;
        }

        [HttpGet("getList")]
        public List<BlogEditModel> GetList()
        {
            var response = _blogService.GetBlogList( _sessionContext.UserSession.UserId);
            if (!response.Any()) ResponseModel.Message = "No Blog's found for you.";
            return response;
        }

        [HttpGet("list")]
        public List<BlogEditModel> List()
        {
            var response = _blogService.List();
            if (!response.Any()) ResponseModel.Message = "No Blog's found.";
            return response;
        }
        [HttpGet("delete/{blogId}")]
        public bool Delete(long blogId)
        {
            var response = _blogService.Delete(blogId,_sessionContext.UserSession.UserId);
            if (!response) ResponseModel.Message = "Unable to delete blog.";
            return response;
        }

    }
}
