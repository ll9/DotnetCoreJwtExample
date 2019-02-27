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
        public MobileUser()
        {

        }

        public MobileUser(string name, string identityUserId)
        {
            Name = name;
            IdentityUserId = identityUserId;
        }

        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public string IdentityUserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
