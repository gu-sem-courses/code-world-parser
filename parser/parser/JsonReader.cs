using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System;
namespace parser
{
    public class JsonReader
    {
        public JsonReader()
        {
        }

        public String XmlToJson(XmlDocument xml) {
            Console.WriteLine("Parser started");
            String json = JsonConvert.SerializeObject(xml);
            JObject processor = JObject.Parse(json);
            JToken data = processor.SelectToken("$.JavaProject.data");
            string result = "{\"data\": "+data.ToString()+"}";
            Console.WriteLine(data);
            Console.WriteLine("Parser closed");
            return result;
        }

        public String ParseSrcML(XmlDocument xml)
        {
            String xJson = JsonConvert.SerializeObject(xml);
            JObject json = JObject.Parse(xJson);
            JObject data = (JObject)json["data"];
            return data.ToString();
        }

        public String ReadSrcML(XmlDocument xml) {
            Console.WriteLine("json parser initiated");
            //make xml into string
            String jsonString = JsonConvert.SerializeObject(xml);

            int bracketCounter = 0;
            string resultString="";

            /*we are just reading out the root tag of the json docuemnt 
             * by looking for the first  open tag  and the last closed tag*/
            for (int i = 0; i < jsonString.Length; i++)
            {
                if (jsonString[i] == '{') 
                {
                    bracketCounter++;
                }
                if (jsonString[i] == '}')
                {
                    bracketCounter--;
                }
                if (bracketCounter > 1)
                {
                    resultString += jsonString[i];
                }
            }
            Console.WriteLine("json parser initiated");
            //return the new string
            return resultString; 
        }
    }
}
