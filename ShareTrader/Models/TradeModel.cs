using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShareTrader.Models
{
    public class TradeModel
    {
        [Key]
        public int Id { get; set; }

        public string ShareId { get; set; }

        public DateTime DateTime { get; set; }

        public string SellerId { get; set; }

        public string BuyerId { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }
    }

    public class TraderContext : DbContext
    {
        public DbSet<TradeModel> Trades { get; set; }

        public TraderContext() : base("name=DefaultConnection")
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