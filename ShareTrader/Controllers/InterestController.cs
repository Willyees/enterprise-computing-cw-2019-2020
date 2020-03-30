using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
        public async Task<IHttpActionResult> PostShareInterest([FromBody]InterestedShareInModel entity)
        {
            try
            {
                string user = "";

                if (Request.Headers.Contains("Authorization"))
                {
                    InterestedShareModel pref = new InterestedShareModel(entity);
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
                            pref.UserId = user;
                        }
                    }
                    using (var requestMessage =
                    new HttpRequestMessage(HttpMethod.Get, "Share?symbol=" + entity.ShareSymbol))
                    {
                        requestMessage.Headers.Authorization =
                            new AuthenticationHeaderValue(scheme, authorization);
                        HttpResponseMessage response = await _client.SendAsync(requestMessage);
                        if (response.IsSuccessStatusCode)
                        {
                            string id = await response.Content.ReadAsStringAsync();
                            pref.ShareId = Convert.ToInt32(id);
                        }
                        //get shareid using the symbol

                        _service.Add(pref);

                    }


                }
                return Ok();
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

        [Route("api/Interest/BrokerNotification")]
        public void PostBrokerNotification([FromBody] BrokerModel brokerModified)
        {
            _service.UpdateBrokers(brokerModified);
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

        [Authorize]
        [Route("api/Interest/Shares")]
        //get the interested shares from the logged user id
        public async Task<ICollection<ShareModel>> GetShares()
        {
            
            string userid = await RetreiveUserId();
            ICollection<int> shareids = _service.GetByUserId(userid);
            //call the service api to get info by ids
            using (var requestMessage =
                    new HttpRequestMessage(HttpMethod.Post, "Share/Infos"))
            {
                string json = JsonConvert.SerializeObject(shareids);
                var content = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
                requestMessage.Content = content;
                var response = await _client.SendAsync(requestMessage);
                if (response.IsSuccessStatusCode)
                {
                    ICollection<ShareModel> output = await response.Content.ReadAsAsync<ICollection<ShareModel>>();
                    return output;
                }
                return new List<ShareModel>();
                
            }
                
        }

        // POST: api/Interest
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Interest/5
        public void Put(int id, [FromBody]string value)
        {
        }

        //deleting the interest by shareid and logged user id
        // DELETE: api/Interest/5
        public async Task<IHttpActionResult> Delete(int shareid)
        {
            string userid = await RetreiveUserId();
            if (!_service.Delete(shareid, userid))
                return BadRequest();
            return Ok();
        }

        //have to check if the request obj is lost when passing to other fucntion
        private async Task<string> RetreiveUserId()
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
                            return user;
                        }
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                return "";
            }

        }
    }
}
