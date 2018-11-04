using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthAPiCore.Data
{
    public class MapData
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Location { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
    }
}
