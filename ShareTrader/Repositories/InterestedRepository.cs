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
    }
}