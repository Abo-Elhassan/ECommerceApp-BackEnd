using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs.Account
{
    public class RegisterDto
    {
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
     
        [Required]
        [EmailAddress]
        public string Email { get; set; }
      
        [Required]
        public string Password { get; set; }
        [RegularExpression("01[0-9]{9}")]
        public string PhoneNumber { get; set; }
        
       
    }
}
