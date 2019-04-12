using HotelApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Services
{
    public class ChooseTheHotel
    {
        public bool checkBron = false;
        DateTime dateTime = new DateTime();
        Rooms rooms = new Rooms()
        {
            TypesOfRooms = typesOfRooms.Бизнес,
            CountOfRooms = 4,
            Price = 50000
        };
        Hotels hotel = new Hotels()
        {
            Id = 1,
            Name = "Akka Antedon Hotel",


        };
        public void HotelChoose()
        {
            Console.WriteLine("Выберите отель: ");
            Console.WriteLine($"Первый отель: \nId: {hotel.Id} \nName: {hotel.Name} \nStars: ");
            int id = int.Parse(Console.ReadLine());
            if(id == 1)
            {
                Console.WriteLine("Вы выбрали первый отель.");
                Console.WriteLine("Выберите комнату: ");
                Console.WriteLine($"Комнаты: \nТип комнаты: {rooms.TypesOfRooms} \nЧисло комнат: {rooms.CountOfRooms} \nЦена: {rooms.Price}");
                int roomId = int.Parse(Console.ReadLine());
                if(roomId == 1)
                {
                    Console.WriteLine("На какую дату вы хотите забронировать отель?");

                    dateTime = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine($"Ваша бронь поставлена на: {dateTime}");
                    checkBron = true;
                }
            }
        }
        public void BronList()
        {
            if (checkBron) {
            Console.WriteLine($"Ваша бронь стоит в отеле: {hotel.Name}" +
                $"\nКомната: {rooms.TypesOfRooms}" +
                $"\nЦена комнаты: {rooms.Price}" +
                $"\nНа дату: {dateTime}");
            }
            else
            {
                Console.WriteLine("У вас нет брони");
            }
        }
        public void Payment()
        {
            Console.WriteLine($"Счёт к оплате: {rooms.Price}");
        }
    }
}
