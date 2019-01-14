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

            foreach(JObject unit in json.SelectTokens("$.data").Children()) {

                //attributes
                JToken attr = unit.SelectToken("$.attributes");
                 if (attr.GetType() == typeof(Newtonsoft.Json.Linq.JObject) || attr.GetType() == typeof(Newtonsoft.Json.Linq.JValue))
                {
                    /*if its not an array then there can be two casses
                    such that it can be a "[]" or a single value that needs to be an array*/
                    if (attr.ToString() == "[]")
                    {
                        attr.Replace(new JArray());
                    }
                    else
                    {
                        JObject newAttr = new JObject();
                        newAttr.Add("accessModifier", attr.SelectToken("$.accessModifier").ToString());
                        newAttr.Add("type", attr.SelectToken("$.type").ToString());
                        newAttr.Add("name", attr.SelectToken("$.name").ToString());
                        JArray res = new JArray();
                        res.Add(newAttr);
                        attr.Replace(res);
                    }
                }

                //methods
                JToken meth = unit.SelectToken("$.methods");
                if (meth.GetType() == typeof(Newtonsoft.Json.Linq.JObject) || meth.GetType() == typeof(Newtonsoft.Json.Linq.JValue))
                {
                    /*if its not an array then there can be two casses
                    such that it can be a "[]" or a single value that needs to be an array*/
                    if (meth.ToString() == "[]")
                    {
                        meth.Replace(new JArray());
                    }
                    else
                    {
                        JObject newAttr = new JObject();
                        newAttr.Add("accessModifier", meth.SelectToken("$.accessModifier").ToString());
                        newAttr.Add("returnType", meth.SelectToken("$.returnType").ToString());
                        newAttr.Add("name", meth.SelectToken("$.name").ToString());
                        JArray res = new JArray();
                        res.Add(newAttr);
                        meth.Replace(res);
                    }
                }

                //subclass
                JToken subclass = unit.SelectToken("$.subclasses");
                if(subclass.Type != JTokenType.Array) {
                    /*if its not an array then there can be two casses
                    such that it can be a "[]" or a single value that needs to be an array*/
                    if(subclass.ToString() == "[]") {
                        subclass.Replace(new JArray());
                    }
                    else { 
                        JArray res = new JArray();
                        res.Add(subclass.ToString());
                        subclass.Replace(res);
                    }
                }

                //associations
                JToken associations = unit.SelectToken("$.associations");
                if (associations.Type != JTokenType.Array)
                {
                    /*if its not an array then there can be two casses
                    such that it can be a "[]" or a single value that needs to be an array*/
                    if (associations.ToString() == "[]")
                    {
                        associations.Replace(new JArray());
                    }
                    else
                    {
                        JArray res = new JArray();
                        res.Add(associations.ToString());
                        associations.Replace(res);
                    }
                }
            }
        }
    }
}
