using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using ShareTrader.Models;
using ShareTrader.Repositories;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace ShareTrader.Services
{
    public class ShareService
    {
        private WatcherShare watcher = new WatcherShare(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-ShareTrader-20200316103418;",
            "https://localhost:44309/api/");
        private ShareRepository _repository = new ShareRepository();


        public ICollection<ShareOutViewModel> GetAll()
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

    public class WatcherShare : IDisposable
    {
        private string _connString;
        private SqlTableDependency<ShareModel> _dependency;
        private HttpClient _client;


        public WatcherShare(string connectionString, string baseAddressNotificationService)
        {
            System.Diagnostics.Debug.WriteLine("intit watcher");
            //var commandDb = @"SELECT [MessageID], [Message], [EmptyMessage], [Date] FROM [dbo].[Messages]";
            // ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
            RegisterForNotification(connectionString);
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddressNotificationService);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }


        public void RegisterForNotification(string connectionString)
        {
            _connString = connectionString;
            _dependency = new SqlTableDependency<ShareModel>(_connString, "ShareModels");
            _dependency.OnChanged += dependency_OnChange;
            _dependency.OnError += dependency_OnError;
            _dependency.Start();
        }


        private void dependency_OnChange(object sender, RecordChangedEventArgs<ShareModel> e)
        {
            //get new data
            System.Diagnostics.Debug.WriteLine("share changed");
            var changedEntity = e.Entity;
            string json = JsonConvert.SerializeObject(changedEntity);
            System.Diagnostics.Debug.WriteLine(changedEntity.Symbol);
            sendRequest(json);
            //dont have to reregister
            //RegisterForNotification();
            //call the stored service to inform of changed share
            
        }

        private void dependency_OnError(object sender, ErrorEventArgs e)
        {
            throw e.Error;
        }

        private async void sendRequest(string json)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Interest/ShareNotification"))
            {
                //requestMessage.Headers.Authorization = new AuthenticationHeaderValue(scheme, authorization);
                var content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                requestMessage.Content = content;
                var reponse = await _client.SendAsync(requestMessage);
            }
        }

        protected void Dispose(bool dispose)
        {
            if (dispose)
            {
                _dependency.Stop();
                _dependency.Dispose();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
