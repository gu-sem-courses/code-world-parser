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

        public String readSrcML(XmlDocument xml) {
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
            Console.WriteLine("json parser complete");
            //return the new string
            return resultString; 
        }
    }
}
