using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;


[Serializable]


class Program
{
    protected static void Main(string[] args)
    {
        /* //Read the XML file. This get me the entire XML file.
         XmlReader r = XmlReader.Create("parsing.xml");
         while (r.NodeType != XmlNodeType.Element)
             r.Read();
         XElement er = XElement.Load(r);
         Console.WriteLine(er);
         */
        /*
        XmlReader r = XmlReader.Create("parsing.xml");
            while (r.Read())

         {
            if (r.NodeType == XmlNodeType.Element)
         {
             //Console.WriteLine(r.LocalName);
            }
        }*/



        XmlDocument xd = new XmlDocument();
        xd.Load("parsing.xml");
        //XmlNodeList nl = xd.SelectNodes("parsing.xml");
        //XmlNode root = nl[0];

        for (int i = 0; i < xd.ChildNodes.Count; i++)
        {

            string name = xd.Name;
            Console.WriteLine("SWAG.----------------1111");
            Console.WriteLine(name);


        }

        for (int j = 0; j < xd.ChildNodes.Count; j++)
        {
            //EITHER TRY TO FIX THIS OR USE THIS 
            //https://support.microsoft.com/en-ca/help/307548/how-to-read-xml-from-a-file-by-using-visual-c

            if (xd.HasAttribute("name"))
            {
                String name = xd.GetAttribute("name");
                Console.WriteLine(name);
            }

        }

        for (int k = 0; k < xd.ChildNodes.Count; k++)
        {
             if (xd.ChildNodes[k].HasChildNodes)
             {
                    Console.WriteLine("SWAG.---------------3333");
                    Console.WriteLine(xd.ChildNodes[k].InnerXml);

             }
        }
            //Console.WriteLine(xd.Chil);




        /*  XmlDocument doc = new XmlDocument();
          doc.Load("parsing.xml");
          XmlElement root = doc.DocumentElement;
          XmlNodeList nodes = root.SelectNodes("some_node"); // You can also use XPath here
          foreach (XmlNode node in nodes)
          {
              // use node variable here for your beeds
          }

          Console.WriteLine(nodes);
          */


        try
        {
            //String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "producedJSON/test2.json");
            String path = "../../producedJSON/test2.json";
            // serialize JSON to a string and then write string to a file
            File.WriteAllText(path, JsonConvert.SerializeObject(xd));
            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, xd);
            }
        }

        catch (Exception u)
        {
            Console.WriteLine(u.ToString());
        }

    }
}


/* 
// This is how the parsing would look like 
//This is an example. I was too lazy to save the website so I just put em here
string xml = @"<?xml version='1.0' standalone='no'?>
<root>
  <person id='1'>
  <name>Alan</name>
  <url>http://www.google.com</url>
  </person>
  <person id='2'>
  <name>Louis</name>
  <url>http://www.yahoo.com</url>
  </person>
</root>";

XmlDocument doc = new XmlDocument();
doc.LoadXml(xml);

string json = JsonConvert.SerializeXmlNode(doc);

Console.WriteLine(json);
// {
//   "?xml": {
//     "@version": "1.0",
//     "@standalone": "no"
//   },
//   "root": {
//     "person": [
//       {
//         "@id": "1",
//         "name": "Alan",
//         "url": "http://www.google.com"
//       },
//       {
//         "@id": "2",
//         "name": "Louis",
//         "url": "http://www.yahoo.com"
//       }
//     ]
//   }
// }
*/

