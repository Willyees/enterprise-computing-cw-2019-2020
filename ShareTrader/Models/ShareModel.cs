using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ShareTrader.Models
{
    public class ShareModel
    {


        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int Volume { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public string Currency { get; set; }

        public string Type { get; set; }

        /*public ShareModel() : this(0, "", 0.0, 0, -1.0, -1.0, "", "")
        {

        }

        public ShareModel(int id, string name, double price, int volume, double high, double low, string currency, string type)
        {
            Id = id;
            Name = name;
            Price = price;
            Volume = volume;
            High = high;
            Low = low;
            Currency = currency;
            Type = type;
        }*/
    }

    public class InterestedShareModel
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("Share")]
        public int ShareId { get; set; }
        public ShareModel Share { get; set; }

    }


    public class ShareContext : DbContext
    {
        public DbSet<ShareModel> Shares {get;set;}
        public DbSet<InterestedShareModel> Interests { get; set; }

        public ShareContext() : base("name=DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}