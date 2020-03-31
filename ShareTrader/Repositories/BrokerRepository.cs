using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using ShareTrader.Models;

namespace ShareTrader.Repositories
{
    public class BrokerRepository : IDisposable, IRepostiory<BrokerModel>
    {
        BrokerContext db = new BrokerContext();

        public ICollection<BrokerModel> GetAll()
        {
            /*return db.Brokers.Select(e => new BrokerOutViewModel
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                Expertise = e.Expertise
            }).ToList();*/
            return db.Brokers.ToList();
        }

        public BrokerModel GetById(int id)
        {
            var Broker = db.Brokers.FirstOrDefault(c => c.Id == id);
            if (Broker == null)
            {
                return null;
            }
            return Broker;
        }

        public ICollection<BrokerModel> GetByName(string firstName, string lastName)
        {
            ICollection<BrokerModel> brokers = db.Brokers.Where(c => (c.FirstName == firstName && c.LastName == lastName)).ToList();
            if (brokers.Count == 0)
            {
                return null;
            }
            return brokers;
        }

        internal ICollection<BrokerModel> GetByEmail(string email)
        {
            ICollection<BrokerModel> brokers = db.Brokers.Where(c => (c.Email == email)).ToList();
            if (brokers.Count == 0)
            {
                return null;
            }
            return brokers;
        }

        internal ICollection<BrokerModel> GetByExpertise(string expertise)
        {
            //todo modify to use a join linq functionality
            var brokers_exp = db.Expertises.Where(e => e.Expertise == expertise).ToList();
            List<BrokerModel> brokers = new List<BrokerModel>();
            foreach(var broker in brokers_exp)
            {
                brokers.Add(GetById(broker.BrokerId));
            }
            return brokers;
        }

        //getall orderded by brokerid 
        internal ICollection<ExpertiseBrokers> OrderByExpertise(ICollection<string> expertises)
        {
            var i_expertises = db.Expertises.OrderBy(e => e.BrokerId).ToList();
            return i_expertises;
        }

        public void Add(BrokerModel entity)
        {
            db.Brokers.Add(entity);
            SaveChanges();
        }

        public void Update(BrokerModel entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(int id)
        {
            var Broker = db.Brokers.FirstOrDefault(c => c.Id == id);
            if (Broker != null)
            {
                db.Brokers.Remove(Broker);
                SaveChanges();
            }
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}