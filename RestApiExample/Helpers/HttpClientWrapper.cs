using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace RestApiExample.Helpers
{
    public class HttpClientWrapper
    {
        public HttpResponseMessage Response(Request request)
        {
            //ignore certificate
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            using var client = new HttpClient(handler);
            var message = new HttpRequestMessage
            {
                Method = request.Method,
                RequestUri = new Uri(request.HostUrl + request.Endpoint),
            };

            //add from data
            if (request.FormData.Count > 0)
            {
                client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                message.Content = new FormUrlEncodedContent(request.FormData);
            }

            //add token
            if (request.Token != null)
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", request.Token);
            }

            //add payload 
            if (request.Payload != null)
            {
                message.Content = new StringContent(request.Payload, Encoding.UTF8, "application/json");
            }
            var responce = client.SendAsync(message).Result;

            if ((int)responce.StatusCode == request.ExpectedStatusCode || request.ExpectedStatusCode == 0)
            {
                //for easy debug
                var json = responce.Content.ReadAsStringAsync().Result;
                return responce;
            }
            throw new Exception($"Expected code:{request.ExpectedStatusCode}, but was {(int)responce.StatusCode}");
        }
    }
}