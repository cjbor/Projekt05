using RethinkDb.Driver;
using RethinkDb.Driver.Net;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClickCounterDB
{
    public class CounterRepo : ICounterRepo
    {
        private readonly Connection _con;
        private RethinkDB _rDB;
        public CounterRepo(IConnectionFactory conFac)
        {
            _con = conFac.CreateConnection();
            _rDB = new RethinkDB();
        }

        public void Count(string hash)
        {
            _rDB.Db("Count").Table("Count").Insert(new Fingerprint { Hash = hash }).Run(_con);
        }
    }
}
