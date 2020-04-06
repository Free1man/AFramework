using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;

namespace RestApiExample.Json
{
    public class JsonPayloadProcessor
    {
        public string ProcessJson(string payloadStringFromFeatureFile)
        {
            var isJsonObject = payloadStringFromFeatureFile.StartsWith('{') && payloadStringFromFeatureFile.EndsWith('}');
            var isJsonArray = payloadStringFromFeatureFile.StartsWith('{') && payloadStringFromFeatureFile.EndsWith(']');
            var isJsonString = isJsonObject || isJsonArray;
            var isJsonFileName = payloadStringFromFeatureFile.Contains(".json");

            if (isJsonFileName)
            {
                var pathToJsonFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                using (var file = File.OpenText(pathToJsonFolder + "//json//" + payloadStringFromFeatureFile))
                using (var reader = new JsonTextReader(file))
                {
                    return JToken.ReadFrom(reader).ToString();
                }
            }
            else if (isJsonString)
            {
                return payloadStringFromFeatureFile;
            }
            throw new Exception($"{payloadStringFromFeatureFile} is not supported json format");
        }
    }
}
