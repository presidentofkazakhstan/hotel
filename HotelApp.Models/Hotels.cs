using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Models
{
    public class Hotels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int Price { get; set; }

        public int Star { get; set; }

    }
}
