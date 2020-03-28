using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using ShareTrader.Models;
using ShareTrader.Services;

namespace ShareTrader.Controllers
{
    //this controller is part of the notification service
    public class InterestController : ApiController
    {
        NotificationService _service;
        private HttpClient _client;

        InterestController() : base()
        {
            System.Diagnostics.Debug.WriteLine("init Interest Controller");
            _service = NotificationService.Instance;
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44309/api/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //POST api/Interest/ShareInterest
        [Authorize]
        [Route("api/Interest/ShareInterest")]
        public async Task<IHttpActionResult> PostShareInterest([FromBody]InterestedShareModel entity)
        {
            try
            {
                string user = "";

                if (Request.Headers.Contains("Authorization"))
                {
                    //getting the user id from the User service and then passing it to be stored in the share interested table
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
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, "error: " + e.Message);
            }
        }

        //POST api/Interested/ShareNotification
        //used to receive the modified share and successively contact the interested clients
        [Route("api/Interest/ShareNotification")]
        //these should be protected with some kind of token since are inter services communications. No user should be able to query them
        public void PostShareNotification([FromBody] ShareModel shareModified)
        {
            _service.NotifyShareChanges(shareModified);
        }


        // GET: api/Interest
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Interest/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Interest
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Interest/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Interest/5
        public void Delete(int id)
        {
        }
    }
}
