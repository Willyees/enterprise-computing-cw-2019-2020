using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareTrader.Models
{
    public class ShareOutViewModel
    {
        public string Symbol { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int Volume { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public string Currency { get; set; }

        public string Type { get; set; }

        public ShareOutViewModel()
        {

        }

        public ShareOutViewModel(string symbol, string name, double price, int volume, double high, double low, string currency, string type)
        {
            Symbol = symbol;
            Name = name;
            Price = price;
            Volume = volume;
            High = high;
            Low = low;
            Currency = currency;
            Type = type;
        }

        public ShareOutViewModel(ShareModel share) : this(share.Symbol, share.Name, share.Price, share.Volume, share.High, share.Low, share.Currency, share.Type)
        {
            
        }
    }
}