using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ShareTrader.Models;
using ShareTrader.Repositories;

namespace ShareTrader.Services
{
    public class ShareService : IService<ShareModel>
    {
        private ShareRepository _repository = new ShareRepository();


        public ICollection<ShareModel> GetAll()
        {
            return _repository.GetAll();
        }

        public ShareModel GetById(int id)
        {
            return _repository.GetById(id);
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