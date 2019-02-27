using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthAPiCore.Models
{
    public class OneTimePassword
    {
        public OneTimePassword()
        {

        }

        public OneTimePassword(string password, MobileUser mobileUser)
        {
            Password = password;
            MobileUser = mobileUser;
        }

        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Password { get; set; }
        public DateTime Expires { get; set; } = DateTime.Now.AddMinutes(5);
        public bool IsConsumed { get; set; } = false;

        public string MobileUserId { get; set; }
        public MobileUser MobileUser { get; set; }

        public static string GeneratePassword(int length = 8, int step = 4)
        {
            var random = new Random();
            var chars = "ABCDEFGHJKMNPQRSTUVWXYZ23456789";
            var passwordBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                if (i != 0 && i % step == 0)
                {
                    passwordBuilder.Append("-");
                }

                passwordBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return passwordBuilder.ToString();
        }
    }
}
