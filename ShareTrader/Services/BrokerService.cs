using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ShareTrader.Models;
using ShareTrader.Repositories;

namespace ShareTrader.Services
{
    public class BrokerService
    {

        private BrokerRepository _repository = new BrokerRepository();


        public ICollection<BrokerModel> GetAll()
        {
            return _repository.GetAll();
        }

        public BrokerModel GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Add(BrokerModel entity)
        {
            _repository.Add(entity);
        }

        public void Update(BrokerModel entity)
        {
            _repository.Update(entity);
        }

        public BrokerModel ReccomendBroker(string Expertise)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}