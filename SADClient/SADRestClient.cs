using RestSharp;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SADClient
{


    [Serializable]
    public class Auth0Token {
        public string access_token { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }

    }

    /// <summary>
    /// Abstract class for auth0-based rest clients
    /// </summary>
    public abstract class Auth0RestClient : RestClient
    {

        public Auth0RestClient() : base() { }
        public Auth0RestClient(string url) : base(url) { }
        public Auth0RestClient(Uri uri) : base(uri) { }

        protected virtual string _client_id { get; }
        protected virtual string _client_secret { get; }

        protected string Token
        {
            get
            {
                if (_token == null)
                { 
                    var client = new RestClient("https://dev-0tdja7uh.eu.auth0.com/oauth/token");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/json");
                    request.AddParameter("application/json", "{\"client_id\":\""+_client_id+"\",\"client_secret\":\""+_client_secret+"\",\"audience\":\"https://SADAssessment.com\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    var result = client.Deserialize<Auth0Token>(response);
                    _token = result.Data.access_token;
                }

                return _token;

            }

        }
        private string _token = null;



        // Override base Execute method to pass authorization token to the request
        public override IRestResponse Execute(IRestRequest request)
        {
            request.AddHeader("authorization", $"Bearer {Token}");
            return base.Execute(request);
        }

    }

    /// <summary>
    /// BookAppender application implementation
    /// </summary>
    public class BookAppenderClient : Auth0RestClient
    {

        public BookAppenderClient(string url) : base(url) { }

        protected override string _client_id => "KqO9Tidm9UASRuritIKYrXJaAO0s7lqG";
        protected override string _client_secret => "rPPWq6ofnbKyn1x9qH2eC1O08RYrs65VcnLYlGNdh1JuvThU8iIOhhsFYiLRzcmH";
    }

    /// <summary>
    /// BookRemover application implementation
    /// </summary>
    public class BookRemoverClient : Auth0RestClient
    {

        public BookRemoverClient(string url) : base(url) { }

        protected override string _client_id => "8GrvFYx50TiUqixkko57V9mLG1HAEtzb";
        protected override string _client_secret => "efWFBqTQOhC1ryUIXohNqe2dLrxpR2wrbhMWVrZ0Txzr_KzbWrn2hlH1XDvJpPd6";
    }

}
