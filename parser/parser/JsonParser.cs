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
            JObject data = (JObject)processor.SelectToken("$.JavaProject");

            FilterJson(data);

            JToken dataS = processor.SelectToken("$.JavaProject");
            string result = data.ToString();
            return result;
        }

        public void FilterJson(JObject json) {
            /*
            name 
            atribute []
            meth []
            superclass
            subclass []
            associations []
            */

            foreach(JObject unit in json.SelectTokens("$.data").Children()) {

                //subclass
                JToken subclass = unit.SelectToken("$.subclasses");
                if(subclass.Type != JTokenType.Array) {
                    Console.WriteLine(unit.SelectToken("$.name"));
                    Console.WriteLine("NOT AN ARRAY");
                    /*if its not an array then there can be two casses
                    such that it can be a "[]" or a single value that needs to be an array*/
                    if(subclass.ToString() == "[]") {
                        subclass.Replace(new JArray());
                    }
                    else { 
                    Console.WriteLine("its the single item");
                        JArray res = new JArray();
                        res.Add(subclass.ToString());
                        subclass.Replace(res);
                    }
                }

                //associations
                JToken associations = unit.SelectToken("$.associations");
                if (associations.Type != JTokenType.Array)
                {
                    Console.WriteLine(unit.SelectToken("$.name"));
                    Console.WriteLine("NOT AN ARRAY");
                    /*if its not an array then there can be two casses
                    such that it can be a "[]" or a single value that needs to be an array*/
                    if (associations.ToString() == "[]")
                    {
                        associations.Replace(new JArray());
                    }
                    else
                    {
                        Console.WriteLine("its the single item");
                        JArray res = new JArray();
                        res.Add(associations.ToString());
                        associations.Replace(res);
                    }
                }
            }
        }
    }
}
