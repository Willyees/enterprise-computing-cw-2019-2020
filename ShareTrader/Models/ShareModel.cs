﻿using System;
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

        public string Symbol { get; set; }

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
    public class ShareQueryModel
    {
        public double Price { get; set; }
        public bool PriceHigher { get; set; }

        public int Amount {get; set;}
        public bool AmountHigher { get; set; }
        
        public ShareQueryModel()
        {
            PriceHigher = false;
            AmountHigher = true;
        }
    }   

    public class ShareContext : DbContext
    {
        public DbSet<ShareModel> Shares {get;set;}

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