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

            var token =
                "OAuth oauth_consumer_key=7EB25C1C55ACC8BEAB8747598075045D, oauth_token=0637202506CF03FF1E34015B74E54B98, oauth_signature_method=PLAINTEXT, oauth_signature=16010BB5026C557453542DC42166FD11%26BB8DD8518DB9FDBEA8FC956A90505826";
            return token;
        }
    }
}