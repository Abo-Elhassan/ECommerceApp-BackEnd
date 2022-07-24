using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class Customer : IdentityUser<Guid>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

    }
}
