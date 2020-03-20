using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ShareTrader.Services;
using ShareTrader.Models;

namespace ShareTrader.Controllers
{
    public class ShareController : ApiController
    {

        ShareService _service = new ShareService();

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

        // POST api/<controller>
        public void Post([FromBody]ShareModel entity)
        {
            _service.Add(entity);
        }

        [Route("api/Share/Interested")]
        public IHttpActionResult PostInterest([FromBody]InterestedShareModel entity)
        {
            try { 
                _service.Add(entity);
                return Ok(entity);
            }
            catch(Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, "error: " + e.Message);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}