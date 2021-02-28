using Blogpost.Core.Application.Domain;

namespace Blogpost.Core.User.Domain
{
    public class Person : DomainBase
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
