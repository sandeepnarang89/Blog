
using Blogpost.Infrasturcture.ORM;

namespace Blogpost.Infrasturcture.Application.Impl
{
    public abstract class BaseRepository
    {
        protected ApplicationDbContext DbContext { get; set; }
    }
}
