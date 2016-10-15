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
            var client = new RestClient("http://example.com");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("scan", Method.POST);
            request.AddParameter("uid", uid); 

            // execute the request
            IRestResponse response = client.Execute(request);
        }
    }
}
