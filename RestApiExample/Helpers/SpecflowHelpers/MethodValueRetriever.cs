using System;
using System.Collections.Generic;
using System.Net.Http;
using TechTalk.SpecFlow.Assist;

namespace RestApiExample.Helpers.SpecflowHelpers
{
    public class MethodValueRetriever : IValueRetriever
    {
        public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            if (keyValuePair.Key == "Method")
            {
                return true;
            }
            return false;

        }

        public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
        {
            return new HttpMethod(keyValuePair.Value);
        }
    }
}
