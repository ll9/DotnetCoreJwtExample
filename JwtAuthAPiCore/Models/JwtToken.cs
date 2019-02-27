using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthAPiCore.Models
{
    public class JwtToken
    {
        public JwtToken()
        {

        }

        public JwtToken(string token, MobileUser mobileUser)
        {
            Token = token;
            MobileUser = mobileUser;
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Token { get; set; }
        public bool Revoked { get; set; } = false;

        public string MobileUserId { get; set; }
        public MobileUser MobileUser { get; set; }
    }
}
