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
            bool isJsonObject = payloadStringFromFeatureFile.StartsWith('{') && payloadStringFromFeatureFile.EndsWith('}');
            bool isJsonArray = payloadStringFromFeatureFile.StartsWith('{') && payloadStringFromFeatureFile.EndsWith(']');
            bool isJsonString = isJsonObject || isJsonArray;
            bool isJsonFileName = payloadStringFromFeatureFile.Contains(".json");

            if (isJsonFileName)
            {
                var pathToJsonFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                using (StreamReader file = File.OpenText(pathToJsonFolder + "//json//" + payloadStringFromFeatureFile))
                using (JsonTextReader reader = new JsonTextReader(file))
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
