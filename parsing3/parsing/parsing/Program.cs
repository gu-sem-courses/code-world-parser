using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;



[Serializable]


class Program
{

    enum NodeTypes
    {
        HasChildren,
        IsNode,
        IsAttribute
    }

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

        /* XmlDocument xd = new XmlDocument();
         xd.Load("parsing.xml");*/

        //--------------------------------------------------------------------------------------------
        //READ FROM HERE GUYS, SORRY FOR THE TOKYO DRIFT HAPPENING ABOVE

        var xd = XDocument.Load("parsing.xml");
        Type type = xd.GetType();
        SpanXDocument(xd.Root);

        


        //get the class name, the attribute name and print it out
        void SpanXDocument(XElement elements)
        {
            NodeTypes nodeType;

            foreach (XElement element in elements.Elements())
            {
                if (element.Descendants().Count() > 0)
                {
                    if (element.Descendants().Descendants().Count() > 0)
                    {
                        nodeType = NodeTypes.HasChildren;
                    }
                    else
                    {
                        nodeType = NodeTypes.IsNode;
                    }
                }
                else
                {
                    nodeType = NodeTypes.IsAttribute;
                }
                switch (nodeType)
                {
                    case NodeTypes.IsNode:
                        Console.WriteLine("Node Name : {0}",
                           element.Name.LocalName);
                        foreach (XElement elementNode in element.Elements())
                        {
                            Console.WriteLine("Node Attribute Name : {0} Value : {1}",
                               elementNode.Name.LocalName, elementNode.Value);
                        }
                        break;
                    case NodeTypes.HasChildren:
                        Console.WriteLine("Has Children Name : {0}",
                           element.Name.LocalName);
                        SpanXDocument(element);
                        break;
                    case NodeTypes.IsAttribute:
                        Console.WriteLine("Attribute Name : {0} Value : {1}",
                           element.Name.LocalName, element.Value);
                        return;
                }


               // Console.WriteLine("SWAG.----------------1111");
            }

        }

        // Code = (string)data.Attribute("name"),





        void test()
        {
            Console.WriteLine("LMAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAO");

            //get the attributes name and type
            var distinctResults = xd.Descendants("result")
                                         .Select(element => element.Attribute("value").Value)
                                         .Distinct();

            foreach (var result in distinctResults)
            {
                Console.WriteLine(result);
                Console.WriteLine("IT IS NOT PRINITING FUCK");
            }
        }





        //for (int k = 0; k < xd.ChildNodes.Count; k++)
        //{
        //     if (xd.ChildNodes[k].HasChildNodes)
        //     {
        //            Console.WriteLine("SWAG.---------------3333");
        //            Console.WriteLine(xd.ChildNodes[k].InnerXml);

        //     }
        //}
        ////Console.WriteLine(xd.Chil);




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



//Lmao this doesn't work fuck me 
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

