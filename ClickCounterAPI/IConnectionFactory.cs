using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RethinkDb.Driver;
using RethinkDb.Driver.Net;

namespace ClickCounterAPI
{
    public interface IConnectionFactory
    {
         Connection CreateConnection();
    }
}
