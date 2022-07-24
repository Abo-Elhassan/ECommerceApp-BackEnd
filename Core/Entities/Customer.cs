using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Customer : IdentityUser<Guid>
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is Required ")]
        [StringLength(20)]
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

    }
}
