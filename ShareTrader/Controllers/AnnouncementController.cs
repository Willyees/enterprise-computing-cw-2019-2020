using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ShareTrader.Models;
using ShareTrader.Services;

namespace ShareTrader.Controllers
{
    public class AnnouncementController : ApiController
    {
        AnnouncementService _service = new AnnouncementService();

        // GET api/<controller>
        public IEnumerable<AnnouncementModel> Get()
        {
            var announcements = _service.GetAll();
            return announcements;
            //todof shoudl get the share symbol to display instead of the id
            /*
            List<AnnouncementOutModel> output = new List<AnnouncementOutModel>();
            foreach(var announcement in announcements)
            {}*/
        }

        // GET api/<controller>/5
        public AnnouncementModel Get(int id)
        {
            return _service.GetById(id);
        }

        // POST api/<controller>
        public void Post([FromBody] AnnouncementModel announcement)
        {
            _service.Add(announcement);
        }

        // PUT api/<controller>/
        public void Put([FromBody]AnnouncementModel announcement)
        {
            _service.Update(announcement);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}