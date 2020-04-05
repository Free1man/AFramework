using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestApiExample.Helpers;
using RestApiExample.Helpers.SpecflowHelpers;
using RestApiExample.Json;
using System.Collections.Generic;
using System.Net.Http;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RestApiExample
{
    [Binding]
    public sealed class Steps
    {
        private readonly ScenarioContext context;
        private HttpResponseMessage response;

        public Steps(ScenarioContext injectedContext)
        {
            context = injectedContext;
            Service.Instance.ValueRetrievers.Register(new MethodValueRetriever());
        }

        [StepDefinition(@"I send a request")]
        public void GivenISendARequest(Table table)
        {
            var request = table.CreateInstance<Request>();
            if (context.ContainsKey("valuesToReplaceInJsonForNextRequest"))
            {
                Dictionary<string, string> valuesToReplaceInJsonForNextRequest = (Dictionary<string, string>)context["valuesToReplaceInJsonForNextRequest"];
                foreach (var valueToUpdate in valuesToReplaceInJsonForNextRequest)
                {
                    request.Payload = JsonEditor.UpdateJson(request.Payload, valueToUpdate.Key, valueToUpdate.Value);
                }
                context.Remove("valuesToReplaceInJsonForNextRequest");
            }
            response = new HttpClientWrapper().Response(request);
        }

        [StepDefinition(@"response should contain")]
        public void ThenJsonResponseShouldContain(Table table)
        {
            var json = response.Content.ReadAsStringAsync().Result;
            var jToken = JToken.Parse(json);

            foreach (var row in table.Rows)
            {
                var jsonPath = row[1];
                var actualValue = jToken.SelectToken(jsonPath).ToString();
                var expectedValue = row[2];
                Assert.AreEqual(expectedValue, actualValue, "Assert failed: " + row[0]);
            }
        }

        [StepDefinition(@"response should contain, using pattern (.*)")]
        public void ThenJsonResponseShouldContain(string pattern, Table table)
        {
            var json = response.Content.ReadAsStringAsync().Result;
            var jsonObject = JToken.Parse(json);

            foreach (var row in table.Rows)
            {
                //# rework if approach will be accepted
                var jsonPath = pattern;
                string s = jsonPath;
                int start = s.IndexOf("<<");
                int end = s.IndexOf(">>", start);
                string result = s.Substring(start, end - start + 2);
                jsonPath = s.Replace(result, row[1]);
                var actualValue = jsonObject.SelectToken(jsonPath).ToString();
                var expectedValue = row[2];
                Assert.AreEqual(expectedValue, actualValue);
            }
        }

        [StepDefinition(@"I update default values to populate the next request")]
        public void GivenIUpdateNextValuesInJsnoForNextReqest(Table table)
        {
            context.Add("valuesToReplaceInJsonForNextRequest", table.ToDictionary());
        }
    }
}
