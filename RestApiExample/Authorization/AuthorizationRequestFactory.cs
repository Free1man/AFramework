using System;
using RestApi.Test.Helpers;

namespace RestApi.Test.Authorization
{
    public class AuthorizationRequestFactory
    {
        public Request CreateRequestBasedOnUserType(UserTypes userType)
        {
            var request = new Request();
            if (userType == UserTypes.Anonymous)
            {
                request.Payload = "{\r\n  \"clientId\": \"WCqJWnpNatcf3LUCxZmq94pR30sj2OOdBbMoGO8NGrMgUMk6Ogl4EMvLqcykNuGf\"\r\n}";
            }
            else
            {
                throw new Exception("not implemented");
            }

            return request;
        }
    }
}
