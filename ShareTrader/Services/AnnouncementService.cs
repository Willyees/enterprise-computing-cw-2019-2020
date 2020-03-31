using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ShareTrader.Models;
using ShareTrader.Repositories;

namespace ShareTrader.Services
{
    public class AnnouncementService : IService<AnnouncementModel>
    {
        AnnouncementRepository _repository = new AnnouncementRepository();
        private Watcher<AnnouncementModel> watcher = new Watcher<AnnouncementModel>(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-ShareTrader-20200316103418;",
            "https://localhost:44309/api/Interest/AnnouncementNotification", "AnnouncementModels");
        public void Add(AnnouncementModel entity)
        {
            _repository.Add(entity);
        }

        public ICollection<AnnouncementModel> GetAll()
        {
            return _repository.GetAll();
        }

        public AnnouncementModel GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(AnnouncementModel entity)
        {
            _repository.Update(entity);
        }
    }
}