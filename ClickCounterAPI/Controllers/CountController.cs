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

        [HttpGet()]
        public JsonResult Get()
        {
            ICardinalityEstimator<string> estimator = new CardinalityEstimator();
            foreach (Fingerprint finger in _counterRepo.GetFingerprints())
            {
                estimator.Add(finger.Hash);
            }
            return new JsonResult(new { Clicks = estimator.Count()});
        }

        // POST api/<controller>
        [HttpPost()]
        public void Post([FromBody]Fingerprint fingerprint)
        {
            _counterRepo.Count(fingerprint);
        }
    }
}
