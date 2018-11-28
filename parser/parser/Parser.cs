using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Permissions;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

[Serializable]

class Program
{
    enum NodeTypes
    {
        HasChildren,
        IsNode,
        IsAttribute,
        IsMethod
    }

    protected static void Main(string[] args)
    {
        var xmlFile = XDocument.Load("singleClass.xml");
        Type type = xmlFile.GetType();
        SpanXDocument(xmlFile.Root);

        //get the class name, the attribute name and print it out
        void SpanXDocument(XElement elements)
        {
            NodeTypes nodeType;
            foreach (XElement element in elements.Elements())
            {
                if (element.Descendants().Count() > 0)
                {
                    if(element.Descendants().Descendants().Count() > 0)
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
                    case NodeTypes.HasChildren:
                        Console.WriteLine("Has Children Name : {0}",
                           element.Name.LocalName);
                        SpanXDocument(element);
                        break;
                    case NodeTypes.IsNode:
                        Console.WriteLine("Node Name : {0}",
                           element.Name.LocalName);
                        foreach (XElement elementNode in element.Elements())
                        {
                            Console.WriteLine("Node Attribute Name : {0} Value : {1}",
                               elementNode.Name.LocalName, elementNode.Value);
                        }
                        break;
                    case NodeTypes.IsAttribute:
                        Console.WriteLine("Attribute Name : {0} Value : {1}",
                           element.Name.LocalName, element.Value);
                        break;
                }
            }
        }

        try
        {
            //String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "producedJSON/test2.json");
            String path = "../../../../assets/xml2json.json";
            // serialize JSON to a string and then write string to a file
            File.WriteAllText(path, JsonConvert.SerializeObject(xmlFile));
            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, xmlFile);
            }
        }

        catch (Exception u)
        {
            Console.WriteLine(u.ToString());
        }

    }
}