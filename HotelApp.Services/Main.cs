using HotelApp.Models;
using HotelApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    public class Main
    {
        List<User> user = new List<User>();
        Login login = new Login();
        TelegramBot telegramBot = new TelegramBot();
        ChooseTheHotel theHotel = new ChooseTheHotel();
        Registration registration = new Registration();
        public void Menu()
        {
            Console.WriteLine("1.Вход" +
                "\n2.Регистрация");
            int number = int.Parse(Console.ReadLine());
            if(number == 1)
            {
                login.UserLogin(user);
                if(login._loginSuccesfull == true)
                {
                    Console.Clear();
                    LoginMenu();
                }
                else if(login._loginSuccesfull == false)
                {
                    registration.ValidationCheck(user);
                    registration.SendSmsOrTelegramBot(user);
                }
            }
            else if(number == 2)
            {
                registration.ValidationCheck(user);
                registration.SendSmsOrTelegramBot(user);
                if (registration._registrationSuccesfull == true)
                {
                    Console.Clear();
                    Menu();
                }
                else
                {
                    Console.Clear();
                    Menu();
                }
            }
        }
        public void LoginMenu()
        {
            Console.WriteLine("1.Выбрать отель\n2.Лист броней" + "\n3.Перейти к оплате");
            int lognum = int.Parse(Console.ReadLine());
            try {
            if(lognum == 1)
            {
                theHotel.HotelChoose();
                LoginMenu();
            }
            else if(lognum == 2)
            {
                theHotel.BronList();
                LoginMenu();
            }
            else if(lognum == 3)
            {
                theHotel.Payment();
            }
            }
            catch (ArgumentException)
            {
                throw new ArgumentException();
            }
        }
    }
}
