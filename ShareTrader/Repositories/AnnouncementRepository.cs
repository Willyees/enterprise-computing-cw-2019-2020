using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using ShareTrader.Models;


namespace ShareTrader.Repositories
{
    public class AnnouncementRepository : IRepostiory<AnnouncementModel>, IDisposable
    {
        AnnouncementContext db = new AnnouncementContext();

        public ICollection<AnnouncementModel> GetAll()
        {
            return db.Announcements.ToList();
        }


        public void Add(AnnouncementModel entity)
        {
            db.Announcements.Add(entity);
            SaveChanges();
        }

       
        public AnnouncementModel GetById(int id)
        {
            return db.Announcements.Where(e => e.Id == id).FirstOrDefault();
        }

       
        public void Update(AnnouncementModel entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
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