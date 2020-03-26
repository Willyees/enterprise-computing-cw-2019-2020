using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ShareTrader.Models;
using ShareTrader.Repositories;
using ShareTrader.Hubs;

namespace ShareTrader.Services
{
    //check if can move all teh outer methods in the hub 
    public class NotificationService : IService<InterestedShareModel>
    {
        private InterestedRepository _repository = new InterestedRepository();

        private readonly static Lazy<NotificationService> _instance = new Lazy<NotificationService>(
        () => new NotificationService(GlobalHost.ConnectionManager.GetHubContext<NotificationHub>().Clients));

        private NotificationService(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
        }

        public static NotificationService Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        //internal methods

        public void Add(InterestedShareModel entity)
        {
            _repository.Add(entity);
        }


        //outside methods towards clients
        //passing the shareid that was modified. Check which user is interested and notify it
        public void NotifyShareChanges(int shareId)
        {
            
        }

        public ICollection<InterestedShareModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public InterestedShareModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(InterestedShareModel entity)
        {
            throw new NotImplementedException();
        }
    }
}