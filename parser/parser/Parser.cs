using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml;

[Serializable]

class Program
{
    protected static void Main(string[] args)
    {
        //Create an xml doc for existing file
        XmlDocument originalXmlFile = new XmlDocument();
        //load project
        originalXmlFile.Load("singleClass.xml");
        XmlNode root = originalXmlFile.DocumentElement;

        //create xml doc for the filtered xml file
        XmlDocument doc = new XmlDocument();
        XmlNode data = doc.CreateElement("data");

        //retrive the specific xml tags from old xml doc
        XmlNodeList attrs = originalXmlFile.GetElementsByTagName("decl_stmt");
        XmlNodeList meths = originalXmlFile.GetElementsByTagName("function"); 

        //import elements to new xml doc
        XmlNode attributes = doc.CreateElement("attributes");
        XmlNode methods = doc.CreateElement("methods"); 

        attributes.AppendChild(importNodes(attrs, originalXmlFile, doc));
        methods.AppendChild(importNodes(meths, originalXmlFile, doc));

        //append elements to the new xml document
        data.AppendChild(attributes);
        data.AppendChild(methods);

        XmlNode superclasses, subclasses, components, associations;

        Console.WriteLine(data.InnerText);
    }


    //----------------------------------------------------------------------------------------------------------------
    public static XmlNode importNodes(XmlNodeList list, XmlDocument originDoc, XmlDocument destinationDoc){
        //you cant really just reference a node from one doc to another so you need to import them first

        //create a export node that stores import
        XmlNode exportNode = destinationDoc.CreateElement("exportNode");

        foreach(XmlNode node in list){
            exportNode.AppendChild(destinationDoc.ImportNode(node,true));
        }
        return exportNode;
    }

    //WARNING
    //the methods below are only reusable for OUR GROUP PROJECT (DIT3**)

    public static XmlNode getAttributes(XmlNodeList list)
    {
        return null;
    }

    public static XmlNode getMethods(XmlNodeList list)
    {
        return null;
    }

    public static XmlNode getSubClasses(XmlNodeList list, XmlDocument originDoc, XmlDocument destinationDoc)
    {
        return null;
    }

    public static XmlNode getSuperClasses(XmlNodeList list, XmlDocument originDoc, XmlDocument destinationDoc)
    {
        return null;
    }

    public static XmlNode getComponents(XmlNodeList list, XmlDocument originDoc, XmlDocument destinationDoc)
    {
        return null;
    }

    public static XmlNode getAttributes(XmlNodeList list, XmlDocument originDoc, XmlDocument destinationDoc)
    {
        return null;
    }
}