using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Models
{
    public enum typesOfRooms
    {
        Эконом,
        Бизнес,
        Люкс
    }

    public class Rooms
    {
        public typesOfRooms TypesOfRooms { get; set; }
        public int CountOfRooms { get; set; }
        public int Price { get; set; }
    }
}
