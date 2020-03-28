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

        //based on the type fields prensent in the broker, call the right repository method
        public ICollection<BrokerModel> GetInfo(BrokerQueryModel entity)
        {
            if(entity.FirstName != null && entity.LastName != null)
            {
                return _repository.GetByName(entity.FirstName, entity.LastName);
            }
            else if(entity.Email != null)
            {
                return _repository.GetByEmail(entity.Email);
            }
            else if(entity.Expertise != null)
            {
                return _repository.GetByExpertise(entity.Expertise);
            }

            return new List<BrokerModel>();
        }
        public ICollection<BrokerModel> GetByName(string firstName, string lastName)
        {
            return _repository.GetByName(firstName, lastName);
        }

        public void Add(BrokerModel entity)
        {
            _repository.Add(entity);
        }

        public void Update(BrokerModel entity)
        {
            _repository.Update(entity);
        }

        //modify to take into account more factors
        public ICollection<BrokerModel> ReccomendBroker(string Expertise)
        {
            return _repository.GetByExpertise(Expertise);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}