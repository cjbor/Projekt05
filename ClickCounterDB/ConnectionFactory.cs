using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RethinkDb.Driver;
using RethinkDb.Driver.Net;

namespace ClickCounterDB
{
    public class ConnectionFactory : IConnectionFactory
    {
        private IConfiguration _config;
        private RethinkDB _dbDriver;

        public ConnectionFactory(RethinkDB dbDriver, IConfiguration config) {
            this._config = config;
            this._dbDriver = dbDriver;
        }

        public Connection CreateConnection()
        {
            var dbSettings = _config.GetSection("connection").Get<DatabaseSettings>();
            return _dbDriver.Connection()
                .Hostname(dbSettings.Hostname)
                .Port(RethinkDBConstants.DefaultPort)
                .Timeout(60).Connect();
        }
    }
}
