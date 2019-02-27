using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthAPiCore.Models
{
    public class OneTimePassword
    {
        [Key]
        public string Id { get; set; }
        public string Password { get; set; }
        public DateTime Expires { get; set; }
        public bool IsConsumed { get; set; }

        public string MobileUserId { get; set; }
        public MobileUser MobileUser { get; set; }
    }
}
