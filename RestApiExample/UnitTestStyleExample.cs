using System;
using System.Linq;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestApi.Test.Helpers;

namespace RestApi.Test
{
    [TestClass]
    public class UnitTestStyleExample
    {
        private const string Token = "OAuth oauth_consumer_key=7EB25C1C55ACC8BEAB8747598075045D, oauth_token=0637202506CF03FF1E34015B74E54B98, oauth_signature_method=PLAINTEXT, oauth_signature=16010BB5026C557453542DC42166FD11%26BB8DD8518DB9FDBEA8FC956A90505826";

        private readonly JToken _jToken;
        

        public UnitTestStyleExample()
        {
            var request = new Request
            {
                Token = Token,
                Method = HttpMethod.Get,
                Endpoint = "Categories/UsedCars.json?with_counts=true"
            };

            var response = new HttpClientWrapper().Response(request);
            var json = response.Content.ReadAsStringAsync().Result;
            _jToken = JToken.Parse(json);
        }

        [TestMethod]
        public void NumberNamedBrandsOfUsedCarTest()
        {
            var actualValue = _jToken.SelectToken("$.Subcategories").Count();
            Assert.AreEqual(76, actualValue, "Assert failed: expected 76 car brands, but was " + actualValue);
        }

        [TestMethod]
        public void KiaBrandCountTest()
        {
            string actualKiaJsonNode = null;
            try
            {
                actualKiaJsonNode = _jToken.SelectToken("$.Subcategories[?(@.Name=='Kia')]").ToString();
            }
            catch (NullReferenceException)
            {
                Assert.IsNotNull(actualKiaJsonNode, "Assert failed: Kia brand is missing");
            }
            string actualValueCount;
            try
            {
                actualValueCount = _jToken.SelectToken("$.Subcategories[?(@.Name=='Kia')].Count").ToString();
            }
            catch (NullReferenceException)
            {
                //in TradeMe if count field is not present it means 0 cars of this brand.
                actualValueCount = "0";
            }
            Assert.AreEqual("0", actualValueCount, "Assert failed: 0 KIA car expected, but was " + actualValueCount);
        }

        [TestMethod]
        public void HispanoSuizaDoesNotExistTest()
        {
            string actualValue = null;
            try
            { 
                actualValue = _jToken.SelectToken("$.Subcategories[?(@.Name=='Hispano Suiza')]").ToString();
            }
            catch (NullReferenceException)
            {
                //do nothing in this case, assert will handle the result
            }
            Assert.IsNull(actualValue, "Assert failed: 'Hispano Suiza' is presented in the list");
        }
    }
}