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

        public double Max_price { get; set; }

        public double Min_price { get; set; }

        //cant use foreign key because these two tables should be located in different DBs
        //[ForeignKey("Share")]
        public int ShareId { get; set; }
        //public ShareModel Share { get; set; }

    }

    public class InterestedShareQuery
    {
        public int ShareId { get; set; }
        public double Max_price { get; set; }

        public double Min_price { get; set; }
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
            base.OnModelCreating(modelBuilder);
        }
    }
}