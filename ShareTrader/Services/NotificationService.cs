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
        public void NotifyShareChanges(ShareModel shareModified)
        {

            //debug
            InterestedShareQuery shareQuery = new InterestedShareQuery
            {
                ShareId = shareModified.Id,
                ActualPrice = shareModified.Price
            };

            ShareOutViewModel outViewShare = new ShareOutViewModel(shareModified);

            ICollection<InterestedShareModel> usersToNotify = _repository.GetInfoInterestedShare(shareQuery);
            foreach(InterestedShareModel shareModel in usersToNotify)
            {
                Clients.User(shareModel.UserId).receiveStockMessage("the share you are interested " + shareModel.ShareId + " has moved price into your interest");
            }
            UpdateShares(outViewShare);
        }

        public void NotifyAnnouncements(AnnouncementModel announcement)
        {
            //get user interested to the share related to the new announcement
            ICollection<InterestedShareModel> interests = _repository.GetInfoInterestedShare(announcement.ShareId);
            foreach(InterestedShareModel interest in interests)
            {
                Clients.User(interest.UserId).receiveAnnouncement("a new announcement related to an interested share has been published!");
            }
            //push change to all the clients screen in real time
            UpdateAnnouncements(announcement);
        }

        //functions to update single entity on client side
        public void UpdateBrokers(BrokerModel broker)
        {
            BrokerOutViewModel outViewBroker = new BrokerOutViewModel(broker);
            Clients.All.updateBrokers(outViewBroker);
        }

        public void UpdateShares(ShareOutViewModel outViewShare)
        {
            //try to update the table to all the users in real time
            Clients.All.updateStockPrice(outViewShare);
        }

        public void UpdateAnnouncements(AnnouncementModel announcement)
        {
            Clients.All.updateAnnouncement(announcement);
        }
     

        public ICollection<InterestedShareModel> GetAll()
        {
            throw new NotImplementedException();
        }

        //return the interested shareids for the provided userid
        public ICollection<int> GetByUserId(string userid)
        {
            ICollection<InterestedShareModel> list =  _repository.GetInfoInterestedShare(userid);
            //only pass the shareids to the controller that will contact share api to retreive the collection of infoshares
            ICollection<int> shareids = new List<int>();
            foreach(InterestedShareModel share in list)
            {
                shareids.Add(share.ShareId);
            }
            return shareids;
        }

        public void Update(InterestedShareModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int shareId, string userId)
        {
            return _repository.Delete(shareId, userId);
        }

        public InterestedShareModel GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}