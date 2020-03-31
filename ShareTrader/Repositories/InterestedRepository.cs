using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ShareTrader.Models;

namespace ShareTrader.Repositories
{
    public class InterestedRepository : IDisposable
    {
        private InterestedContext db = new InterestedContext();
        public bool Add(InterestedShareModel entity)
        {
            try { 
                db.InterestedShares.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                throw new Exception("error in adding new interested share: " + e.Message);
                
            }
            
        }

        //returning list of shareinterests by share id
        public ICollection<InterestedShareModel> GetInfoInterestedShare(int shareid)
        {
            ICollection<InterestedShareModel> list = db.InterestedShares.Where(c => c.ShareId == shareid).ToList();
            db.SaveChanges();
            return list;
        }

        //returning list of shareinterstes taking into account the current price of share
        public ICollection<InterestedShareModel> GetInfoInterestedShare(InterestedShareQuery interestInfo)
        {
            ICollection<InterestedShareModel> list = db.InterestedShares.Where(c => (c.ShareId == interestInfo.ShareId && (interestInfo.ActualPrice > c.MaxPrice || interestInfo.ActualPrice < c.MinPrice))).ToList();
            db.SaveChanges();
            return list;
        }

        public ICollection<InterestedShareModel> GetInfoInterestedShare(string userid)
        {
            ICollection<InterestedShareModel> list = db.InterestedShares.Where(c => (c.UserId == userid)).ToList();
            return list;
        }

        
        public bool Delete(int shareId, string userId)
        {
            var Shares = db.InterestedShares.Where(c => c.ShareId == shareId && c.UserId == userId).ToList();
            if (Shares.Count > 0)
            {
                foreach(InterestedShareModel share in Shares)
                    db.InterestedShares.Remove(share);
                db.SaveChanges();
                return true;
            }
            return false;
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