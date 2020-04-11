using System.Net.Http;
using RestApi.Test.Helpers;
using RestApi.Test.Json;

namespace RestApi.Test.Authorization
{
    public class TokenProvider
    {
        public string GetTokenBasedOnUserType(UserTypes userType)
        {
            if (userType == UserTypes.Unauthorized)
            {
                return null;
            }
            var request = new AuthorizationRequestFactory().CreateRequestBasedOnUserType(userType);
            request.HostUrl = new ConfigReader().GetConfig().GetSection("AuthorizationUrl").Value;
            request.Method = new HttpMethod("POST");
            var responseJson = new HttpClientWrapper().Response(request).Content.ReadAsStringAsync().Result;
            var token = JsonEditor.GetValueFromJson(responseJson, "$.token.access_token");
            return token;
        }
    }
}