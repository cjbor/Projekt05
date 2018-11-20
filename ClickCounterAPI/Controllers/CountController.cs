using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardinalityEstimation;
using ClickCounterDB;
using Microsoft.AspNetCore.Mvc;
using RethinkDb.Driver;
using RethinkDb.Driver.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClickCounterAPI.Controllers
{
    [Route("api/[controller]")]
    public class CountController
    {
        ICounterRepo _counterRepo;
        public CountController(ICounterRepo counterRepo)
        {
            _counterRepo = counterRepo;
        }

        /*[HttpGet()]
        public JsonResult Get()
        {
            Cursor<Fingerprint> result = _rDB.Db("Count").Table("Count").Run<Fingerprint>(_con);
            ICardinalityEstimator<string> estimator = new CardinalityEstimator();
            foreach (Fingerprint finger in result.BufferedItems)
            {
                estimator.Add(finger.Hash);
            }
            return new JsonResult(new { Clicks = estimator.Count()});
        }*/

        // POST api/<controller>
        [HttpPost("{hash}")]
        public void Post(string hash)
        {
            _counterRepo.Count(hash);
        }
    }
}
