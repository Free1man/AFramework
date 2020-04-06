using RestApiExample.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace RestApiExample.Helpers
{
    public class Request
    {
        private readonly string _apiUnderTestUrl = new ConfigReader().GetConfig().GetSection("ApiUrlUnderTest").Value;

        private string _endpoint;
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

                var jsonPayloadProcessor = new JsonPayloadProcessor();
                return jsonPayloadProcessor.ProcessJson(_payload);
            }
            set => _payload = value;
        }

        public string Endpoint { get => _apiUnderTestUrl + _endpoint; set => _endpoint = value; }

        public int ExpectedStatusCode { get; set; }

        public Dictionary<string, string> FormData { get; set; } = new Dictionary<string, string>();
    }
}