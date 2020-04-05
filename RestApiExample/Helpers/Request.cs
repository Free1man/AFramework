using RestApiExample.Json;
using System.Collections.Generic;
using System.Net.Http;

namespace RestApiExample.Helpers
{
    public class Request
    {
        private readonly string APIUnderTestUrl = new ConfigReader().GetConfig().GetSection("ApiUrlUnderTest").Value;

        private string endpoint;
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

        public string Endpoint { get => APIUnderTestUrl + endpoint; set => endpoint = value; }

        public int ExpectedStatusCode { get; set; }

        public Dictionary<string, string> FormData { get; set; } = new Dictionary<string, string>();
    }
}