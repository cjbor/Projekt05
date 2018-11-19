using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // POST api/<controller>
        [HttpPost("{fingerprint}")]
        public void Post(string fingerprint , [FromBody]string value)
        {
            _rDB.Db("Count").Table("Count").Insert(new { Fingerprint = fingerprint}).Run(_con);
        }
    }
}
