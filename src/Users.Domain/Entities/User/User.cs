using Users.Domain.Base;
using Users.Domain.ValueObjects;

namespace Users.Domain.Entities
{
    public partial class User : BaseEntity
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public EmailAddress EmailAddress { get; private set; }
        public bool Active { get; private set; }
        public string ActivationCode { get; private set; }
        
    }
}