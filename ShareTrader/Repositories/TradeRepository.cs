using ShareTrader.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace ShareTrader.Repositories
{
    public class TradeRepository : IDisposable, IRepostiory<TradeModel>
    {
        private TraderContext db = new TraderContext();

        public ICollection<TradeModel> GetAll()
        {
            return db.Trades.ToList();
        }

        // GET: api/Trade/5
        
        public TradeModel GetById(int id)
        {
            TradeModel tradeModel = db.Trades.Find(id);
            
            return tradeModel;
        }

            public ICollection<TradeModel> Get(TradeQueryModel q)
        {
            //check that each field was set from the user. If is null/empty/default value, then output true so it will not taken into account for the equality
            return db.Trades.Where(e =>(
            (q.LowerBoundDate == default(DateTime) ? true : q.LowerBoundDate < e.DateTime) &&
            (q.UpperBoundDate == default(DateTime) ? true : q.UpperBoundDate > e.DateTime) &&
            (String.IsNullOrEmpty(q.ShareSymbol) ? true : q.ShareSymbol == e.ShareId) && 
            (String.IsNullOrEmpty(q.SellerId) ? true : q.SellerId == e.SellerId) && 
            (String.IsNullOrEmpty(q.BuyerId) ? true : q.BuyerId == e.BuyerId))).ToList();
            //return new List<TradeModel>();
        }

        //losing the return bool from the inner update call. Should change the Interface declaration (to use bool return)
        public void Update(TradeModel tradeModel)
        {
            Update(tradeModel.Id, tradeModel);
        }

        public bool Update(int id, TradeModel tradeModel)
        {
            db.Entry(tradeModel).State = EntityState.Modified;

            try
            {
                SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public void Add(TradeModel tradeModel)
        {
            db.Trades.Add(tradeModel);
            SaveChanges();

        }

        public bool Delete(int id)
        {
            TradeModel tradeModel = db.Trades.Find(id);
            if (tradeModel == null)
            {
                return false;
            }

            db.Trades.Remove(tradeModel);
            SaveChanges();

            return true;
        }

        private bool Exists(int id)
        {
            return db.Trades.Count(e => e.Id == id ) > 0;
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
            try { 
                db.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception("coulnt save changes to db: " + e.Message);
            }
        }

    }
}