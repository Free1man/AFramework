using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestApiExample.Helpers;
using RestApiExample.Helpers.SpecflowHelpers;
using RestApiExample.Json;
using System.Collections.Generic;
using System.Net.Http;
using RestApiExample.Authorization;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RestApiExample
{
    [Binding]
    public sealed class Steps
    {
        private readonly ScenarioContext _context;
        private HttpResponseMessage _response;
        private Dictionary<UserTypes, string> _issuedTokens = new Dictionary<UserTypes, string>();

        public Steps(ScenarioContext injectedContext)
        {
            _context = injectedContext;
            Service.Instance.ValueRetrievers.Register(new MethodValueRetriever());
        }

        [StepDefinition(@"I send a request")]
        public void GivenISendARequest(Table table)
        {
            var request = table.CreateInstance<Request>();
            if (_context.ContainsKey("valuesToReplaceInJsonForNextRequest"))
            {
                var valuesToReplaceInJsonForNextRequest = (Dictionary<string, string>)_context["valuesToReplaceInJsonForNextRequest"];
                foreach (var valueToUpdate in valuesToReplaceInJsonForNextRequest)
                {
                    request.Payload = JsonEditor.UpdateJson(request.Payload, valueToUpdate.Key, valueToUpdate.Value);
                }
                _context.Remove("valuesToReplaceInJsonForNextRequest");
            }

            if (_issuedTokens.ContainsKey(request.UserType))
            {
                _issuedTokens.TryGetValue(request.UserType, out var oldToken);
                request.Token = oldToken;
            }
            _issuedTokens.TryAdd(request.UserType, request.Token);

            //TODO: need to be revisited
            if (request.Endpoint.Contains("{{"))
            {
                var endpoint = request.Endpoint;
                int start = endpoint.IndexOf("{{");
                int end = endpoint.IndexOf("}}", start);
                string result = endpoint.Substring(start + 2, end - start - 2);
                _context.TryGetValue(result, out string newValue);
                endpoint = endpoint.Replace("{{", "").Replace("}}","");
                request.Endpoint = endpoint.Replace(result, newValue);
            }
            _response = new HttpClientWrapper().Response(request);
        }

        [StepDefinition(@"response should contain")]
        public void ThenJsonResponseShouldContain(Table table)
        {
            var json = _response.Content.ReadAsStringAsync().Result;
            var jToken = JToken.Parse(json);

            foreach (var row in table.Rows)
            {
                var jsonPath = row[1];
                var actualValue = jToken.SelectToken(jsonPath).ToString();
                var expectedValue = row[2];
                Assert.AreEqual(expectedValue, actualValue, "Assert failed: " + row[0]);
            }
        }

        [StepDefinition(@"I update default values to populate the next request")]
        public void GivenIUpdateNextValuesInJsonForNextRequest(Table table)
        {
            _context.Add("valuesToReplaceInJsonForNextRequest", table.ToDictionary());
        }

        [Then(@"I save value from json for future reuse")]
        public void ThenISaveValueFromJsonForFutureReuse(Table table)
        {
            var json = _response.Content.ReadAsStringAsync().Result;
            var jToken = JToken.Parse(json);

            foreach (var row in table.Rows)
            {
                var actualValue = jToken.SelectToken(row[1]).ToString();
                _context.Add(row[0], actualValue);
            }
        }
    }
}
