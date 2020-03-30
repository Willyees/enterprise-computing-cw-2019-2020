using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ShareTrader.Models
{
    public class InterestedShareModel
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public double MaxPrice { get; set; }

        public double MinPrice { get; set; }

        //cant use foreign key because these two tables should be located in different DBs
        //[ForeignKey("Share")]
        public int ShareId { get; set; }
        //public ShareModel Share { get; set; }
        public InterestedShareModel()
        {

        }

        public InterestedShareModel(InterestedShareInModel inmodel)
        {
            MaxPrice = inmodel.MaxPrice;
            MinPrice = inmodel.MinPrice;
        }
    }

    public class InterestedShareInModel
    {
        public double MaxPrice { get; set; }

        public double MinPrice { get; set; }
        public string ShareSymbol { get; set; }
    }

    public class InterestedShareQuery
    {
        public int ShareId { get; set; }
        public double ActualPrice { get; set; }
    }

    public class InterestedContext : DbContext
    {
        public DbSet<InterestedShareModel> InterestedShares { get; set; }

        public InterestedContext() : base("name=DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<InterestedContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}