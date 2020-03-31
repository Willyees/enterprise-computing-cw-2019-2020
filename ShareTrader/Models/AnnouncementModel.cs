using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShareTrader.Models
{
    public class AnnouncementModel
    {
        public int Id { get; set; }
        public string Announcement { get; set; }
        public string Type { get; set; }
        //public string ShareHolderId { get; set; }
        public int ShareId { get; set; }
    }

    public class AnnouncementOutModel
    {
        public int Id { get; set; }
        public string Announcement { get; set; }
        public string Type { get; set; }
        //public string ShareHolderId { get; set; }
        public string ShareSymbol { get; set; }
    }

    public class AnnouncementContext : DbContext
    {
        public DbSet<AnnouncementModel> Announcements { get; set; }

        public AnnouncementContext() : base("name=DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}