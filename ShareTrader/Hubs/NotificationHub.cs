using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

using ShareTrader.Models;

namespace ShareTrader.Hubs
{
    public class NotificationHub : Hub
    {
        public string Test()
        {
            return "sup?";
        }

        public async Task SendMessageTest()
        {
            await Clients.All.SendAsync("receiveStockMessage", " ? !WHASYP ? ");
        }
        public void Hello()
        {
            Clients.All.hello();
        }

        public async Task NotifyStockChanged(string stockName)
        {
            await Clients.All.SendAsync("receiveStockMessage", stockName);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }


        /*public void GetUsers()
        {
            List<ShareModel> _lst = new List<ShareModel>();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ADOEntities"].ConnectionString))
            {
                String query = "SELECT Id, Name, Price FROM dbo.ShareModels";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Notification = null;
                    DataTable dt = new DataTable();
                    SqlDependency dependency = new SqlDependency(command);

                    dependency.OnChange += dependency_OnChange;

                    if (connection.State == ConnectionState.Closed) connection.Open();

                    SqlDependency.Start(connection.ConnectionString);
                    var reader = command.ExecuteReader();
                    dt.Load(reader);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            _lst.Add(new ShareModel
                            {
                                Id = Int32.Parse(dt.Rows[i]["Id"].ToString()),
                                Name = dt.Rows[i]["Name"].ToString(),
                                Price = dt.Rows[i]["Price"].ToString()
                            });
                        }
                    }
                }
            }
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<RealtimeDataHub>();
            context.Clients.All.displayUsers(_lst);
        }

        void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                RealtimeDataHub _dataHub = new RealtimeDataHub();
                _dataHub.GetUsers();
            }
        }*/

    }
}