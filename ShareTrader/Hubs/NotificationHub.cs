using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;


using ShareTrader.Models;
using ShareTrader.Services;

namespace ShareTrader.Hubs
{
    public class NotificationHub : Hub
    {
        private NotificationService _service;

        public NotificationHub() :
            this(NotificationService.Instance)
        {

        }

        public NotificationHub(NotificationService notificationService)
        {
            _service = notificationService;
        }

        public void Info()
        {
            System.Diagnostics.Debug.WriteLine("info");
            //System.Diagnostics.Debug.WriteLine(Context.ConnectionId);
            //Clients.User("a@a.com").send("receiveStockMessage", "WHASYT");
            //Clients.User("e@e.com").receiveStockMessage("WHASYT");
            
            //_service.NotifyShareChanges(5);
            //Clients.Caller.receiveStockMessage("Asad");
            System.Diagnostics.Debug.WriteLine("end");
        }

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
        

        public Task JoinGroup(string groupName)
        {
            return Groups.Add(Context.ConnectionId, groupName);
        }

        public Task LeaveGroup(string groupName)
        {
            return Groups.Remove(Context.ConnectionId, groupName);
        }


        public override Task OnConnected()
        {
            //everytime user will change page, is disconnected and then reconnected, changing connection id. Have to set here again
            System.Diagnostics.Debug.WriteLine("onconnected");
            return base.OnConnected();
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