﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs.Account
{
    public class TokenDto
    {
        public string Token { get; set; }
        public DateTime Exp { get; set; }
    }
}
