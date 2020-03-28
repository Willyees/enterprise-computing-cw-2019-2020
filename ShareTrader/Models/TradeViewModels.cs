using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareTrader.Models
{
    public class TradeQueryModel
    {
        public DateTime LowerBoundDate { get; set; } 
        public DateTime UpperBoundDate { get; set; }
        public string ShareSymbol { get; set; }
        public string SellerId { get; set; }
        public string BuyerId { get; set; }
    }

    public class TradeOutViewModel
    {
        public DateTime DateTime { get; set; }

        public int SellerId { get; set; }

        public int BuyerId { get; set; }

        public double Price { get; set; }

        public int Amount { get; set; }
    }
}