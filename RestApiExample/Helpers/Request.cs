using System.Collections.Generic;
using System.Net.Http;
using RestApi.Test.Authorization;
using RestApi.Test.Json;

namespace RestApi.Test.Helpers
{
    public class Request
    {

        public string HostUrl { get; set; } = new ConfigReader().GetConfig().GetSection("ApiUrlUnderTest").Value;

        private string _payload;
        private string _token;

        public HttpMethod Method { get; set; }

        public string Payload
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_payload))
                {
                    return _payload;
                }

                JsonPayloadProcessor jsonPayloadProcessor = new JsonPayloadProcessor();
                return jsonPayloadProcessor.ProcessJson(_payload);
            }
            set => _payload = value;
        }

        public string Endpoint { get; set; }

        public int ExpectedStatusCode { get; set; }

        public UserTypes UserType { get; set; }

        public string Token
        {

            get
            {
                if (_token != null)
                {
                    return _token;
                }
                _token = new TokenProvider().GetTokenBasedOnUserType(UserType);
                return _token;
            }
            set => _token = value;
        }

        public Dictionary<string, string> FormData { get; set; } = new Dictionary<string, string>();
    }
}