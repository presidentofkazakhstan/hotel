using HotelApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Services
{
    public class Login
    {
        private string _userName;
        private string _password;
        public bool _loginSuccesfull;
        Registration registration = new Registration();
        public void UserLogin(List<User> users)
        {
            Console.WriteLine("Введите имя пользователя: ");
            _userName = Console.ReadLine();
            Console.WriteLine("Введите пароль: ");
            _password = registration.ReadPassword();
            for (int i = 0; i < users.Count; i++)
            {
                if (_userName == users[i].Login)
                {
                    if (_password == users[i].Password)
                    {
                        _loginSuccesfull = true;
                    }
                }
            }
        }
        }
    }
