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

        public void Add(ShareModel entity)
        {
            _repository.Add(entity);
        }

        public void Add(InterestedShareModel entity)
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