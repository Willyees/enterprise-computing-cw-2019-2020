using ShareTrader.Models;
using ShareTrader.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShareTrader.Services
{
    public class TradeService : IService<TradeModel>
    {
        private TradeRepository _repository = new TradeRepository();

        public void Add(TradeModel entity)
        {
            _repository.Add(entity);
        }

        public ICollection<TradeModel> GetAll()
        {
            return _repository.GetAll();
        }

        public TradeModel GetById(int id)
        {
            return _repository.GetById(id);
        }

        //general query.todo: modify return type to tradeoutviewmodel
        public ICollection<TradeModel> Get(TradeQueryModel query)
        {
            return _repository.Get(query);
        }

        public void Update(TradeModel entity)
        {
            _repository.Update(entity);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}