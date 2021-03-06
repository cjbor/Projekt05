﻿using RethinkDb.Driver;
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

        public void Count(Fingerprint fingerprint)
        {
            _rDB.Db("Count").Table("Count").Insert(fingerprint).Run(_con);
        }

        public List<Fingerprint> GetFingerprints()
        {
            Cursor<Fingerprint> result = _rDB.Db("Count").Table("Count").Run<Fingerprint>(_con);
            return result.BufferedItems;
        }
    }
}
