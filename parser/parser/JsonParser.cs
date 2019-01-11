using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System;
namespace parser
{
    public class JsonParser
    {
        public String XmlToJson(XmlDocument xml)
        {
            String json = JsonConvert.SerializeObject(xml);
            JObject processor = JObject.Parse(json);
            //JToken data = processor.SelectToken("$.JavaProject.data");
            //string result = "{\"data\": " + data.ToString() + "}";

            JToken data = processor.SelectToken("$.JavaProject");
            string result = data.ToString();
            return result;
        }

        public String filterJson(String json) {
            /*
            name 
            atribute []
            meth []
            superclass
            subclass []
            associations []
            */


            return null;
        }
    }
}
