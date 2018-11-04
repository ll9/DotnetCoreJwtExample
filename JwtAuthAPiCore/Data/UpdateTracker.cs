using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthAPiCore.Data
{
    public class UpdateTracker
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int LastUpdatedId { get; set; }
    }
}
