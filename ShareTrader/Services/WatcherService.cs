using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ShareTrader.Services
{
    //extension service to be added to existing services. Will monitor their Db context and send api request over to user specified endpoint with modified information retreived from DB
    public class Watcher<P> : IDisposable where P : class, new()
    {
        private string _connString;
        private string _tableName;
        private SqlTableDependency<P> _dependency;
        private HttpClient _client;
        private string _url;


        public Watcher(string connectionString, string urlNotificationApi, string tableName)
        {
            System.Diagnostics.Debug.WriteLine("intit watcher");
            // ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
            RegisterForNotification(connectionString, tableName, urlNotificationApi);
            _client = new HttpClient();
            //_client.BaseAddress = new Uri(baseAddressNotificationService);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }


        public void RegisterForNotification(string connectionString, string tableName, string urlNotificationApi)
        {
            _connString = connectionString;
            _tableName = tableName;
            _url = urlNotificationApi; 
            _dependency = new SqlTableDependency<P>(_connString, _tableName);
            _dependency.OnChanged += dependency_OnChange;
            _dependency.OnError += dependency_OnError;
            _dependency.Start();
        }


        private void dependency_OnChange(object sender, RecordChangedEventArgs<P> e)
        {
            //get new data
            System.Diagnostics.Debug.WriteLine("Db updated");
            var changedEntity = e.Entity;
            string json = JsonConvert.SerializeObject(changedEntity);
            sendRequest(json);
        }

        private void dependency_OnError(object sender, ErrorEventArgs e)
        {
            throw e.Error;
        }

        private async void sendRequest(string json)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, _url))
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