using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using ShareTrader.Models;

namespace ShareTrader.Repositories
{
    public class BrokerRepository
    {
        BrokerContext db = new BrokerContext();

        public ICollection<BrokerModel> GetAll()
        {
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

        public void Add(BrokerModel entity)
        {
            db.Brokers.Add(entity);
            db.SaveChanges();
        }

        public void Update(BrokerModel entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var Broker = db.Brokers.FirstOrDefault(c => c.Id == id);
            if (Broker != null)
            {
                db.Brokers.Remove(Broker);
                db.SaveChanges();
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
    }
}