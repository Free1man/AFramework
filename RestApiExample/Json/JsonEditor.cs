using Newtonsoft.Json.Linq;

namespace RestApi.Test.Json
{
    public static class JsonEditor
    {
        public static string UpdateJson(string json, string jsonPath, string newValue)
        {
            var jsonO = JObject.Parse(json);
            jsonO.SelectToken(jsonPath).Replace(newValue);
            return jsonO.ToString();
        }

        public static string GetValueFromJson(string json, string jsonPath)
        {
            var jsonO = JObject.Parse(json);
            return jsonO.SelectToken(jsonPath).ToString();
        }
    }
}
