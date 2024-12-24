﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopSocialApp.Data.DataModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = null!;

        public string? ProfileImageUrl { get; set; }
    }
}
