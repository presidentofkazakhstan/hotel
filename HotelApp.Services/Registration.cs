using HotelApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace HotelApp.Services
{
    public class Registration
    {
            public bool _registrationSuccesfull = false;
         User user = new User();
        public void ValidationCheck(List<User> users)
        {

            int minimalLengthOfPassword = 8;

            Console.Write("Введите логин: ");
                user.Login = Console.ReadLine();
                try
                {
                    if (user.Login.Contains(" ") | string.IsNullOrEmpty(user.Login))
                    {
                        throw new ArgumentException();
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Логин не соответствует требованиям");
                    throw new ArgumentException();
                }
                Console.Write("Введите e-mail: ");
                user.Email = Console.ReadLine();
                try
                {
                    if (!user.Email.Contains('@') | !user.Email.Contains('.'))
                    {
                        throw new ArgumentException();
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("E-mail не соответствует требованиям");
                    throw new ArgumentException();
                }
                Console.Write("Введите пароль: ");
                user.Password = ReadPassword();
            int lengthPassword = user.Password.Length;
            try
                {
                    if (lengthPassword < minimalLengthOfPassword)
                    {
                        throw new ArgumentException();
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Пароль не соответствует требованиям");
                    throw new ArgumentException();
                }
                try
                {
                    if (!user.Password.Contains('!') && !user.Password.Contains('$') && !user.Password.Contains('%') && !user.Password.Contains(':') && !user.Password.Contains('?') && !user.Password.Contains('&') && !user.Password.Contains('*') && !user.Password.Contains('(') && !user.Password.Contains(')') && !user.Password.Contains('+') && !user.Password.Contains('-') && !user.Password.Contains(']') && !user.Password.Contains('[') && !user.Password.Contains('\'') && !user.Password.Contains('/') && !user.Password.Contains('.') && !user.Password.Contains('?') && !user.Password.Contains('<') && !user.Password.Contains('>'))
                    {
                        throw new ArgumentException();
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Пароль не соответствует требованиям");
                    throw new ArgumentException();
                }
                try
                {
                    if (!user.Password.Any(Char.IsNumber) || !user.Password.Any(Char.IsUpper) || !user.Password.Any(Char.IsLower))
                    {
                        throw new ArgumentException();
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Пароль не соответствует требованиям");
                    throw new ArgumentException();
                }
                Console.Write("Повторите пароль: ");
                user.DoublePassword = ReadPassword();
                try
                {
                    if (user.DoublePassword != user.Password)
                    {
                        throw new ArgumentException();
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Пароли не совпадают");
                    throw new ArgumentException();
                }
                Console.Write("Введите номер телефона: ");
                user.MobileNumber = Console.ReadLine();
                try
                {
                    if (string.IsNullOrEmpty(user.MobileNumber) || user.MobileNumber.Length > 12 || user.MobileNumber.Contains('-') || user.MobileNumber.Contains(' '))
                    {
                        throw new ArgumentException();
                    }
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException("Мобильный телефон неверный");
                    throw new ArgumentException();
                }
            users.Add(user);
        }
            public string ReadPassword()
            {
                string password = "";
                ConsoleKeyInfo info = Console.ReadKey(true);
                while (info.Key != ConsoleKey.Enter)
                {
                    if (info.Key != ConsoleKey.Backspace)
                    {
                        Console.Write("*");
                        password += info.KeyChar;
                    }
                    else if (info.Key == ConsoleKey.Backspace)
                    {
                        if (!string.IsNullOrEmpty(password))
                        {

                            password = password.Substring(0, password.Length - 1);
                            int pos = Console.CursorLeft;
                            Console.SetCursorPosition(pos - 1, Console.CursorTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        }
                    }
                    info = Console.ReadKey(true);
                }
                Console.WriteLine();
                return password;
            }
            public void SendSmsOrTelegramBot(List<User> users)
        {
            TelegramBot telegramBot = new TelegramBot();
            Random rand = new Random();
            string code = Convert.ToString(rand.Next(1000, 9999));
            Console.WriteLine("Куда скинуть код: " +
                        "\n1.СМС код" + "\n2.Телеграм Бот");
            int codeNum = int.Parse(Console.ReadLine());
            if (codeNum == 1)
            {
                var accountSid = "AC2ec5f1cd0280d496beef7f07a50a2b89";
                var authToken = "ec5f9a40c06da9049d54d4237ed90962";
                TwilioClient.Init(accountSid, authToken);
                var to = new PhoneNumber(user.MobileNumber);
                var from = new PhoneNumber("+17372270725");
                var message = MessageResource.Create(
                    to: to,
                    from: from,
                    body: code);
                Console.WriteLine(message.Body);
                Console.Write("Введите свой код: ");
                string checkCode = Console.ReadLine();
                if (checkCode == code)
                {
                    Console.WriteLine("Регистрация прошла успешно!");

                    _registrationSuccesfull = true;
                }
            }
            else if (codeNum == 2)
            {
                Registration registration = new Registration();
                    TelegramBotClient botClient = new TelegramBotClient("467770975:AAF8LBRlJWXnrD6Zj3Dst_yDOOzw-W4ITC4");
                    var me = botClient.GetMeAsync().Result;
                    Console.WriteLine(me.Username);
                    botClient.OnMessage += HandleMessage;
                    botClient.StartReceiving();

                    void HandleMessage(
                        object sender, MessageEventArgs messageEventArgs)
                    {
                        var chatId = messageEventArgs.Message.Chat.Id;
                        botClient.SendTextMessageAsync(chatId, $"Ваш код: {code}");
                    }
                    Console.WriteLine("Ваш код: ");
                    string checkCode = Console.ReadLine();
                    if (checkCode == code)
                    {
                        Console.WriteLine("Регистрация выполнена успешна");
                    }
                    Console.ReadLine();
                }
            }
           
            }

        }
