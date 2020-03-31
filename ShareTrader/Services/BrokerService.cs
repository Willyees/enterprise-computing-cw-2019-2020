using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

using ShareTrader.Models;
using ShareTrader.Repositories;

namespace ShareTrader.Services
{
    public class BrokerService
    {
        private Watcher<BrokerModel> watcher = new Watcher<BrokerModel>(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-ShareTrader-20200316103418;",
           "https://localhost:44309/api/Interest/BrokerNotification", "BrokerModels");
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
        public ICollection<BrokerScoreOutModel> ReccomendBroker(ICollection<string> expertises)
        {
            var i_expertises =  _repository.OrderByExpertise(expertises);
            //dirty way to do it: cannot find a proper way to do with sql query
            //cannot use dictionary because implemented with hash map and not guarantee order of iteration
            List<KeyValuePair<int, int>> dict = new List<KeyValuePair<int, int>>();//key: brokerid, val: count matched expertises
            int previousid = i_expertises.First().BrokerId;
            bool flag = false;
            int count = 0;
            foreach (var e in i_expertises)
            {
                //rest count if new brokerid also add the id to the dictionary
                if (previousid != e.BrokerId)
                {
                    dict.Add(new KeyValuePair<int, int>(previousid, count));
                    previousid = e.BrokerId;
                    count = 0;
                }
                foreach (var t in expertises)
                {
                    if (e.Expertise == t)
                    {
                        flag = true;
                        break;
                    }
                }
                //if true means that the expertise is amongst the provided
                if (flag)
                {
                    count++;
                    //reset the flag for next checks
                    flag = false;
                }
            }
            //have to add  last broker outside the loop
            dict.Add(new KeyValuePair<int, int>(previousid, count));

            //todo: order them by counter, then call db to get the information for each broker id. may be filter by 10, or something.
            var ordered = dict.OrderByDescending(x => x.Value);
            

            //List<KeyValuePair<int, int>> listorder = new List<KeyValuePair<int, int>>();
            //can trim the number of elements in the list order here. todof
            ICollection<BrokerModel> brokers = new List<BrokerModel>();
            List<BrokerScoreOutModel> brokers_score = new List<BrokerScoreOutModel>();

            foreach(KeyValuePair<int,int> entity in ordered)
            {
                var broker = _repository.GetById(entity.Key);
                var score = entity.Value * 10 + broker.QualityGrade;
                BrokerScoreOutModel outview = new BrokerScoreOutModel(broker) { Score = score };
                brokers_score.Add(outview);
            
            }

            brokers_score.OrderByDescending(x => x.Score);
            return brokers_score;


        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}