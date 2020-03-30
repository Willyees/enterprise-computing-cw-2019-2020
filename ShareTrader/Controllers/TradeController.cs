using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ShareTrader.Models;
using ShareTrader.Repositories;
using ShareTrader.Services;

namespace ShareTrader.Controllers
{
    public class TradeController : ApiController
    {
        private TradeService _service = new TradeService();

        // GET: api/Trade
        public ICollection<TradeModel> GetTrades()
        {
            return _service.GetAll();
        }

        // GET: api/Trade/5
        public IHttpActionResult GetTrade(int id)
        {
            TradeModel tradeModel = _service.GetById(id);
            if (tradeModel == null)
            {
                return NotFound();
            }

            return Ok(tradeModel);
        }

        [Route("api/Trade/Info")]
        public IHttpActionResult PostTradeInfo([FromBody] TradeQueryModel query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ICollection<TradeModel> output = _service.Get(query);
            return Ok(output);
        }
        // PUT: api/Trade/5
        public void PutTrade([FromBody] TradeModel tradeModel)
        {
            _service.Update(tradeModel);
        }

        // POST: api/Trade
        public IHttpActionResult PostTrade([FromBody] TradeModel tradeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _service.Add(tradeModel);

            return Ok(tradeModel);
        }

        // DELETE: api/Trade/5
        public IHttpActionResult DeleteTrade(int id)
        {
            bool success =_service.Delete(id);
            if (!success)
            {
                return NotFound();
            }

            return Ok("Successfully removed trade " + id);
        }

       
    }
}