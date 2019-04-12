using HotelApp.Models;
using HotelApp.DataAccess;
using HotelApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    class Program
    {
        static void Main(string[] args)
        {
            //Main main = new Main();
            //main.Menu();

            var dataService = new UsersHotelDataService();


            var _login = Console.ReadLine();
            var _password = Console.ReadLine();
            var _mobileNumber = Console.ReadLine();
            var _email = Console.ReadLine();

 
            dataService.AddUser(new User
            {
                Login = _login,
                Password = _password,
                MobileNumber = _mobileNumber,
                Email = _email



            });


            Console.ReadLine();
        }
    }
}
