using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using ShareTrader.Models;

namespace ShareTrader.Repositories
{
    public class ShareRepository : IDisposable
    {
        ShareContext db = new ShareContext();

        public ICollection<ShareOutViewModel> GetAll()
        {
            return db.Shares.Select(e => new ShareOutViewModel { 
            Symbol = e.Symbol, Name = e.Name , Price = e.Price, Volume = e.Volume, High = e.High, Low = e.Low, Currency = e.Currency, Type = e.Type
            }).ToList();
        }

        public ShareModel GetById(int id)
        {
            var Share = db.Shares.FirstOrDefault(c => c.Id == id);
            if (Share == null)
            {
                return null;
            }
            return Share;
        }

        public ICollection<ShareModel> GetByPrice(double price, bool higher)
        {
            if (higher)
            {
                var sharesHigher = db.Shares.Where(c => (price > c.Price));
                return sharesHigher.ToList();
            }
            var sharesLower = db.Shares.Where(c => (price < c.Price));
            return sharesLower.ToList();
        }

        public ICollection<ShareModel> GetByAmount(int amount, bool higher)
        {
            if (higher)
            {
                var sharesHigher = db.Shares.Where(c => (amount > c.Volume));
                return sharesHigher.ToList();
            }
            var sharesLower = db.Shares.Where(c => (amount < c.Volume));
            return sharesLower.ToList();
        }

        public void Add(ShareModel entity)
        {
            db.Shares.Add(entity);
            db.SaveChanges();
        }


        public void Update(ShareModel entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var Share = db.Shares.FirstOrDefault(c => c.Id == id);
            if (Share != null)
            {
                db.Shares.Remove(Share);
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