using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDReader
{
    internal class RFIDSender
    {
        internal void SendRFID(string uid)
        {
            var client = new RestClient("https://ytv3odwce7.execute-api.us-west-2.amazonaws.com/prod/");

            var request = new RestRequest("ScanItem", Method.GET);
            request.AddParameter("ProductId", uid); 

            // execute the request
            IRestResponse response = client.Execute(request);
        }
    }
}
