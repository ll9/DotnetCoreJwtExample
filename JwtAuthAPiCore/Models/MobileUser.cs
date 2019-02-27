using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthAPiCore.Models
{
    public class MobileUser
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        public string IdentityUserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
