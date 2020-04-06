using RestApiExample.Authorization;
using RestApiExample.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace RestApiExample.Helpers
{
    public class Request
    {

        public string HostUrl { get; set; } = new ConfigReader().GetConfig().GetSection("ApiUrlUnderTest").Value;

        private string payload;
        private string token;

        public HttpMethod Method { get; set; }

        public string Payload
        {
            get
            {
                if (string.IsNullOrWhiteSpace(payload))
                {
                    return payload;
                }

                JsonPayloadProcessor jsonPayloadProcessor = new JsonPayloadProcessor();
                return jsonPayloadProcessor.ProcessJson(payload);
            }
            set => payload = value;
        }

        public string Endpoint { get; set; }

        public int ExpectedStatusCode { get; set; }

        public UserTypes UserType { get; set; }

        public string Token
        {

            get
            {
                token = new TokenProvider().GetTokenBasedOnUserType(UserType);
                return token;
            }
            set => token = value;
        }

        public Dictionary<string, string> FormData { get; set; } = new Dictionary<string, string>();
    }
}