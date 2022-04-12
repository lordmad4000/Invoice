using Users.Domain.Base;
using Users.Domain.ValueObjects;

namespace Users.Domain.Entities
{
    public partial class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EmailAddress EmailAddress { get; set; }
        public bool Active { get; set; }
        public string ActivationCode { get; set; }
        
    }
}