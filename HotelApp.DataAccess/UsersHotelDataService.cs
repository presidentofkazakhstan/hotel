using System;
using System.Collections.Generic;
using HotelApp.Models;
using System.Data.Common;
using System.Configuration;




namespace HotelApp.DataAccess
{
    public class UsersHotelDataService
    {
        private readonly string _connectionString;
        private readonly string _providerName;
        private readonly DbProviderFactory _providerFactory;


        public UsersHotelDataService()
        {
            
            _connectionString = ConfigurationManager.ConnectionStrings["testConnectionString"].ConnectionString;

            _providerName = ConfigurationManager.ConnectionStrings["testConnectionString"].ProviderName;

            _providerFactory = DbProviderFactories.GetFactory(_providerName);
        }

        

        public List<Hotels> GetAllHotels()
        {
            var data = new List<Hotels>();

            using (var connection =_providerFactory.CreateConnection())

            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();
                    command.CommandText = "select * from Hotels";

                    var DataReader = command.ExecuteReader();

                    while (DataReader.Read())
                    {
                        int id = (int)DataReader["Id"];
                        string name = DataReader["Name"].ToString();
                        string city = DataReader["City"].ToString();
                        int price = (int)DataReader["Price"];
                        int star =(int)DataReader["Star"];

                        data.Add(new Hotels
                        {
                            Id = id,
                            Name = name,
                            City = city,
                            Price = price,
                            Star = star
                        });
                    }
                    DataReader.Close();
                }
                catch (DbException exception)
                {
                    //TODO обработка ошибки
                    throw;
                }
                
            }
            return data;
        }

        public List<User> GetAllUsers()
        {
            var data = new List<User>();

            using (var connection = _providerFactory.CreateConnection())

            using (var command = connection.CreateCommand())
            {
                try
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();
                    command.CommandText = "select * from Users";

                    var DataReader = command.ExecuteReader();

                    while (DataReader.Read())
                    {
                        int id = (int)DataReader["Id"];
                        string login = DataReader["Login"].ToString();
                        string password = DataReader["Password"].ToString();
                        string mobileNumber = DataReader["MobileNumber"].ToString();

                        data.Add(new User
                        {
                            Id = id,
                            Login = login,
                            Password = password,
                            MobileNumber = mobileNumber
                        });
                    }
                    DataReader.Close();
                }
                catch (DbException exception)
                {
                    //TODO обработка ошибки
                    throw;
                }

            }
            return data;
        }

        public void AddHotel(Hotels hotel)
        {
            using (var connection = _providerFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                DbTransaction transaction = null;

                try
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();

                    transaction = connection.BeginTransaction();

                    command.CommandText = $"insert into Hotels values('{hotel.Name}', '{hotel.City}'  , '{hotel.Price}' ,  '{hotel.Star}')";
                    command.Transaction = transaction;

                    DbParameter nameParameter = command.CreateParameter();

                    nameParameter.ParameterName = "@name";
                    nameParameter.Value = hotel.Name;

                    nameParameter.DbType = System.Data.DbType.String;
                    nameParameter.IsNullable = false;

                    DbParameter cityParameter = command.CreateParameter();

                    cityParameter.ParameterName = "@city";
                    cityParameter.Value = hotel.City;

                    cityParameter.DbType = System.Data.DbType.String;
                    cityParameter.IsNullable = false;

                    DbParameter priceParameter = command.CreateParameter();

                    priceParameter.ParameterName = "@price";
                    priceParameter.Value = hotel.Price;

                    priceParameter.DbType = System.Data.DbType.Int64;
                    priceParameter.IsNullable = false;

                    DbParameter starParameter = command.CreateParameter();

                    starParameter.ParameterName = "@star";
                    starParameter.Value = hotel.Star;

                    starParameter.DbType = System.Data.DbType.String;
                    starParameter.IsNullable = false;

                    command.Parameters.AddRange(new DbParameter[] { nameParameter, cityParameter, priceParameter, starParameter });

                    var affectedRows = command.ExecuteNonQuery();

                    if (affectedRows < 1)
                    {
                        throw new Exception("Вставка не была произведена");
                    }
                    transaction.Commit();
                    transaction.Dispose();
                }
                catch (DbException exception)
                {
                    transaction?.Rollback();
                    transaction.Dispose();
                    //TODO обработка ошибки
                    throw;
                }
                catch (Exception exception)
                {
                    //TODO обработка ошибки
                    throw;
                }
            }
        }

        public void AddUser(User user)
        {
            using (var connection = _providerFactory.CreateConnection())
            using (var command = connection.CreateCommand())
            {
                DbTransaction transaction = null;

                try
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();

                    transaction = connection.BeginTransaction();

                    command.CommandText = $"insert into Users values('{user.Login}', '{user.Password}'  , '{user.MobileNumber}' , '{user.Email}')";
                    command.Transaction = transaction;

                    DbParameter loginParameter = command.CreateParameter();

                    loginParameter.ParameterName = "@login";
                    loginParameter.Value = user.Login;

                    loginParameter.DbType = System.Data.DbType.String;
                    loginParameter.IsNullable = false;

                    DbParameter passwordParameter = command.CreateParameter();

                    passwordParameter.ParameterName = "@password";
                    passwordParameter.Value = user.Password;

                    passwordParameter.DbType = System.Data.DbType.String;
                    passwordParameter.IsNullable = false;


                    DbParameter mobileNumberParameter = command.CreateParameter();

                    mobileNumberParameter.ParameterName = "@mobileNumber";
                    mobileNumberParameter.Value = user.MobileNumber;

                    mobileNumberParameter.DbType = System.Data.DbType.String;
                    mobileNumberParameter.IsNullable = false;

                    command.Parameters.AddRange(new DbParameter[] { loginParameter, passwordParameter, mobileNumberParameter });

                    var affectedRows = command.ExecuteNonQuery();

                    if (affectedRows < 1)
                    {
                        throw new Exception("Вставка не была произведена");
                    }
                    transaction.Commit();
                    transaction.Dispose();
                }
                catch (DbException exception)
                {
                    transaction?.Rollback();
                    transaction.Dispose();
                    //TODO обработка ошибки
                    throw;
                }
                catch (Exception exception)
                {
                    //TODO обработка ошибки
                    throw;
                }
            }
        }
    }
}
