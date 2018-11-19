using RestSharp;
using System;

namespace ClickCounterTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://localhost:52834");
            var countRequest = new RestRequest("Count/{id}", Method.POST);
        }
    }
}
