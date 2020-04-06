using System;
using RestApiExample.Helpers;

namespace RestApiExample.Authorization
{
    public class AuthorizationRequestFactory
    {
        public Request CreateRequestBasedOnUserType(UserTypes userType)
        {
            var request = new Request();
            if (userType == UserTypes.WhiskUser)
            {
                request.Payload = "AuthorizationWhiskUser.json";
            }
            else
            {
                throw new Exception("not implemented");
            }

            return request;
        }
    }
}
