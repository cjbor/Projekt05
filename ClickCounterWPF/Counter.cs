using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClickCounterWPF
{
    class Counter
    {
        RestClient client;

        public Counter()
        {
            client = new RestClient("http://localhost:52834");
        }

        public void Count()
        {
            var request = new RestRequest("api/Count", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { hash = GetHashString(GetMac()) }); // uses JsonSerializer
            client.Execute(request);
        }

        public string GetResult()
        {
            var request = new RestRequest("api/Count", Method.GET);
            var result = client.Execute(request);
            JObject countObj = JObject.Parse(result.Content);
            return (string)countObj["clicks"];
        }

        private string GetMac()
        {
            var macAddr =
            (
                from nic in NetworkInterface.GetAllNetworkInterfaces()
                where nic.OperationalStatus == OperationalStatus.Up
                select nic.GetPhysicalAddress().ToString()
            ).FirstOrDefault();
            return macAddr;
        }

        private byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
