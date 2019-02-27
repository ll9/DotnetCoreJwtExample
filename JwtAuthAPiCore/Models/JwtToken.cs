using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthAPiCore.Models
{
    public class JwtToken
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public bool Revoked { get; set; }

        public string MobileUserId { get; set; }
        public MobileUser MobileUser { get; set; }
    }
}
