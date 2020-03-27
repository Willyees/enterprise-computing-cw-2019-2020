using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using ShareTrader.Models;
using ShareTrader.Repositories;

namespace ShareTrader.Services
{
    public class ShareService : IService<ShareModel>
    {
        private WatcherShare watcher = new WatcherShare(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-ShareTrader-20200316103418;",
            @"SELECT Id, Name, Price, Volume, High, Low, Currency, Type FROM dbo.ShareModels");
        private ShareRepository _repository = new ShareRepository();


        public ICollection<ShareModel> GetAll()
        {
            return _repository.GetAll();
        }

        public ShareModel GetById(int id)
        {
            return _repository.GetById(id);
        }

        public ICollection<ShareModel> GetByPrice(double price, bool higher)
        {
            return _repository.GetByPrice(price, higher);
        }

        public ICollection<ShareModel> GetByAmount(int price, bool higher)
        {
            return _repository.GetByAmount(price, higher);
        }


        public ICollection<ShareModel> GetInfo(ShareQueryModel entity)
        {
            if(Convert.ToBoolean(entity.Price))
            {
                return _repository.GetByPrice(entity.Price, entity.PriceHigher);
            }
            if(entity.AmountHigher)
            {
                return _repository.GetByAmount(entity.Amount, entity.AmountHigher);
            }
            return new List<ShareModel>();
        }

        public void Add(ShareModel entity)
        {
            _repository.Add(entity);
        }


        public void Update(ShareModel entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }

    public class WatcherShare
    {

        readonly string _connString;
        readonly SqlCommand _command;
        private SqlConnection _connection;
        private SqlDependency _dependency;
        //public event ResultChangedEventHandler NewMessage;

        public WatcherShare(string connectionString, string commandDb)
        {
            System.Diagnostics.Debug.WriteLine("intit watcher");
            //var commandDb = @"SELECT [MessageID], [Message], [EmptyMessage], [Date] FROM [dbo].[Messages]";
            _connString = connectionString;// ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
            _connection = new SqlConnection(_connString);
            _command = new SqlCommand(commandDb, _connection);


            /*ICollection<ShareModel> shares = reader.Cast<IDataRecord>()
                .Select(x => new ShareModel()
                {
                    Id = x.GetInt32(0),
                    Name = x.GetString(1),
                    Price = x.GetDouble(2),
                    Volume = x.GetInt32(2),
                    High = x.GetDouble(2),
                    Low = x.GetDouble(2),
                    Currency = x.GetString(2),
                    Type = x.GetString(2),
                }).ToList();*/


            RegisterForNotification();

        }

        private void RegisterForNotification()
        {
            _connection.Open();
            _command.Notification = null;
            _dependency = new SqlDependency(_command);
            _dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
            using (var reader = _command.ExecuteReader())
            {

            }
            _connection.Close();
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            //get new data
            System.Diagnostics.Debug.WriteLine("share changed");
            if (e.Type == SqlNotificationType.Change)
            {
                /*if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                using (var reader = _command.ExecuteReader()) { 
                    ICollection<ShareModel> shares = reader.Cast<IDataRecord>()
                        .Select(x => new ShareModel()
                        {
                            Id = x.GetInt32(0),
                            Name = x.GetString(1),
                            Price = x.GetDouble(2),
                            Volume = x.GetInt32(2),
                            High = x.GetDouble(2),
                            Low = x.GetDouble(2),
                            Currency = x.GetString(2),
                            Type = x.GetString(2),
                        }).ToList();
                }*/
                RegisterForNotification();
                System.Diagnostics.Debug.WriteLine("share changed");
            }
        }
    }
}