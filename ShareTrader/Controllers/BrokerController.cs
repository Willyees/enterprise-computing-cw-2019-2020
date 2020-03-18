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
    public class BrokerController : ApiController
    {

        BrokerService _service = new BrokerService();

        /*public BrokerController(BrokerRepository repository)
        {
            Console.WriteLine("Setting up broker controller with param");
            _repository = repository;
        }*/
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
