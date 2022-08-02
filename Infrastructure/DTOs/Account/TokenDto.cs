using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs.Account
{
    public class TokenDto
    {
        
        public string Value { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
