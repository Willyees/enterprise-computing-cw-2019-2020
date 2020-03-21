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

        [Authorize]
        [Route("api/Share/Interested")]
        public async Task<IHttpActionResult> PostInterest([FromBody]InterestedShareModel entity)
        {
            try {
                string user = "";

                if (Request.Headers.Contains("Authorization"))
                {
                    string authorization = Request.Headers.Authorization.Parameter;
                    string scheme = Request.Headers.Authorization.Scheme;
                    using (var requestMessage =
                    new HttpRequestMessage(HttpMethod.Get, "Account/UserInfo"))
                    {
                        requestMessage.Headers.Authorization =
                            new AuthenticationHeaderValue(scheme, authorization);
                        HttpResponseMessage response = await _client.SendAsync(requestMessage);
                   
                    //HttpResponseMessage response = await _client.GetAsync("");
                    
                        if (response.IsSuccessStatusCode)
                        {
                            string user_str = await response.Content.ReadAsStringAsync();
                            var definition = new { Id = "" };
                            var deserialized = JsonConvert.DeserializeAnonymousType(user_str, definition);
                            user = deserialized.Id;

                            entity.UserId = user;
                            _service.Add(entity);
                        }
                    }

                }

                
                return Ok(user);
            }
            catch(Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, "error: " + e.Message);
            }
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