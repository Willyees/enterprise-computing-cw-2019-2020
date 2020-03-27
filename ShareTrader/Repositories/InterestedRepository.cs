using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ShareTrader.Models;

namespace ShareTrader.Repositories
{
    public class InterestedRepository
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

        //returning list of shareinterstes where the actual price is either less than the minimum or higher than the maximum
        public ICollection<InterestedShareModel> GetInfoInterestedShare(InterestedShareQuery interestInfo)
        {
            ICollection<InterestedShareModel> list = db.InterestedShares.Where(c => (c.ShareId == interestInfo.ShareId && (interestInfo.Max_price > c.Max_price || interestInfo.Min_price < c.Min_price))).ToList();
            return list;
        }
    }
}