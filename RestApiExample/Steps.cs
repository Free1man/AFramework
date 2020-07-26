using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestApi.Test.Authorization;
using RestApi.Test.Helpers;
using RestApi.Test.Helpers.SpecflowHelpers;
using RestApi.Test.Json;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RestApi.Test
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
                string actualValue;
                try
                {
                    actualValue = jToken.SelectToken(jsonPath).ToString();
                }
                catch (NullReferenceException)
                {
                    actualValue = "0";
                }
                var expectedValue = row[2];
                Assert.AreEqual(expectedValue, actualValue, "Assert failed: " + row[0]);
            }
        }

        [StepDefinition(@"response should contain node")]
        public void ThenResponseShouldContainNode(Table table)
        {
            var json = _response.Content.ReadAsStringAsync().Result;
            var jToken = JToken.Parse(json);

            foreach (var row in table.Rows)
            {
                var jsonPath = row[1];
                string actualValue = null;
                try
                {
                    actualValue = jToken.SelectToken(jsonPath).ToString();
                }
                catch (NullReferenceException)
                {
                    //do nothing in this case, assert will handle the result
                }
                Assert.IsNotNull(actualValue, "Assert failed: " + jsonPath + " node is missing");
            }
        }


        [StepDefinition(@"response should not contain node")]
        public void ThenResponseShouldNotContainNode(Table table)
        {
            var json = _response.Content.ReadAsStringAsync().Result;
            var jToken = JToken.Parse(json);

            foreach (var row in table.Rows)
            {
                var jsonPath = row[1];
                string actualValue = null;
                try
                {
                    actualValue = jToken.SelectToken(jsonPath).ToString();
                }
                catch (NullReferenceException)
                {
                    //do nothing in this case, assert will handle the result
                }
                Assert.IsNull(actualValue, "Assert failed: " + jsonPath + " node should not exists");
            }
        }
    }
}
