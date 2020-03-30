using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using ShareTrader.Models;
using ShareTrader.Repositories;


namespace ShareTrader.Services
{
    public class ShareService
    {
        private Watcher<ShareModel> watcher = new Watcher<ShareModel>(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-ShareTrader-20200316103418;",
            "https://localhost:44309/api/Interest/ShareNotification", "ShareModels");
        private ShareRepository _repository = new ShareRepository();


        public ICollection<ShareOutViewModel> GetAll()
        {
            return _repository.GetAll();
        }

        public ShareModel GetById(int id)
        {
            return _repository.GetById(id);
        }

        public ICollection<ShareModel> GetById(ICollection<int> shareids)
        {
            return _repository.GetById(shareids);
        }
        public int GetIdBySymbol(string symbol)
        {
            return _repository.GetIdBySymbol(symbol);
        }

        public ICollection<ShareModel> GetByPrice(double price, bool higher)
        {
            return _repository.GetByPrice(price, higher);
        }

        public ICollection<ShareModel> GetByAmount(int price, bool higher)
        {
            return _repository.GetByAmount(price, higher);
        }


        public ICollection<ShareModel> GetInfo(ShareQueryModel entity)
        {
            if(Convert.ToBoolean(entity.Price))
            {
                return _repository.GetByPrice(entity.Price, entity.PriceHigher);
            }
            if(entity.AmountHigher)
            {
                return _repository.GetByAmount(entity.Amount, entity.AmountHigher);
            }
            return new List<ShareModel>();
        }

        public void Add(ShareModel entity)
        {
            _repository.Add(entity);
        }


        public void Update(ShareModel entity)
        {
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }

    

}
