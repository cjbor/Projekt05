using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardinalityEstimation;
using Microsoft.AspNetCore.Mvc;
using RethinkDb.Driver;
using RethinkDb.Driver.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClickCounterAPI.Controllers
{
    [Route("api/[controller]")]
    public class CountController : ControllerBase
    {
        private readonly Connection _con;
        private RethinkDB _rDB;
        public CountController(IConnectionFactory conFac) {
            _con = conFac.CreateConnection();
            _rDB = new RethinkDB();
        }

        [HttpGet()]
        public JsonResult Get()
        {
            List<Fingerprint> result = _rDB.Db("Count").Table("Count").Run<List<Fingerprint>>(_con);
            ICardinalityEstimator<string> estimator = new CardinalityEstimator();
            foreach (Fingerprint finger in result)
            {
                estimator.Add(finger.Hash);
            }
            return new JsonResult(new { Clicks = estimator.Count()});
        }

        // POST api/<controller>
        [HttpPost("{fingerprint}")]
        public void Post(string fingerprint)
        {
            _rDB.Db("Count").Table("Count").Insert(new Fingerprint { Hash = fingerprint}).Run(_con);
        }
    }
}
