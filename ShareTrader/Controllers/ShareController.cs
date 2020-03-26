using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ShareTrader.Services;
using ShareTrader.Models;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShareTrader.Controllers
{
    public class ShareController : ApiController
    {

        ShareService _service;
        private HttpClient _client; 

        ShareController() : base()
        {
            _service = new ShareService();
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44309/api/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [Route("api/Share/Info")]
        public IEnumerable<ShareModel> GetInfo(ShareQueryModel entity)
        {
            var shares = _service.GetInfo(entity);
            return shares;
        }

        [Authorize(Roles = "Admin")]
        // POST api/<controller>
        public void Post([FromBody]ShareModel entity)
        {
            _service.Add(entity);
        }

       

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}