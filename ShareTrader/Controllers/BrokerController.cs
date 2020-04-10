using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

using ShareTrader.Models;
using ShareTrader.Services;

namespace ShareTrader.Controllers
{
    public class BrokerController : ApiController
    {

        BrokerService _service;
        private HttpClient _client;
        public BrokerController() : base()
        {
            System.Diagnostics.Debug.WriteLine("init Broker Controller");
            _service = new BrokerService();
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44309/api/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        //to use a generic repository, will have to set up Dipendency Injection 



        // GET: api/Broker
        public IEnumerable<BrokerModel> Get()
        {
            return _service.GetAll();
        }

        // GET: api/Broker/5
        public IHttpActionResult Get(int id)
        {
            var broker = _service.GetById(id);
            if(broker != null)
            {
                return Ok(broker);
            }
            return NotFound();
        }

        //GET: api/Broker/Info?name=<name>&surname=<surname>
        //get brokers by names
        [Route("api/Broker/Info")]
        public IEnumerable<BrokerModel> GetInfo(BrokerQueryModel entity)
        {
            return _service.GetInfo(entity);
        }

        [Authorize(Roles = "Admin")]
        // POST: api/Broker
        public IHttpActionResult Post([FromBody]BrokerModel broker)
        {
            try
            {
                _service.Add(broker);
                return Ok(broker);
            }
            catch (Exception e )
            {
                return Content(HttpStatusCode.InternalServerError, "Error Adding Entry. Try again later" + e.Message);
            }
        }

        [Authorize]
        [Route("api/Broker/Reccomend")]
        public async Task<IEnumerable<BrokerScoreOutModel>> GetReccomend()
        {
            //get shares by using the user id
            string user = "";

            if (Request.Headers.Contains("Authorization"))
            {
                //getting the user id from the User service and then passing it to be stored in the share interested table
                string authorization = Request.Headers.Authorization.Parameter;
                string scheme = Request.Headers.Authorization.Scheme;
                using (var requestMessage =
                new HttpRequestMessage(HttpMethod.Get, "Interest/Shares"))
                {
                    requestMessage.Headers.Authorization =
                        new AuthenticationHeaderValue(scheme, authorization);
                    HttpResponseMessage response = await _client.SendAsync(requestMessage);

                    //HttpResponseMessage response = await _client.GetAsync("");

                    if (response.IsSuccessStatusCode)
                    {
                        ICollection<ShareModel> shares_interested = await response.Content.ReadAsAsync<ICollection<ShareModel>>();
                        //todof could use an automapper 
                        List<string> expertises = new List<string>();
                        //only pass the experrty type to the service
                        foreach(var e in shares_interested)
                        {
                            expertises.Add(e.Type);
                        }
                        return _service.ReccomendBroker(expertises);
                        
                    }
                }
            }
            return new List<BrokerScoreOutModel>();
            //api/Interest/Shares
        }

        [Authorize]
        // PUT: api/Broker/5
        public void Put(int id, [FromBody]string value)
        {
        }
        [Authorize( Roles="Admin")]
        // DELETE: api/Broker/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Error Deleting Entry. Try again Later");
            }
        }

    }
}
