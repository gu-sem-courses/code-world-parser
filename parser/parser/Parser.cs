using Newtonsoft.Json;
using System;
using System.Xml;

[Serializable]

class Program
{
    protected static void Main(string[] args)
    {
        parser.SrcReader reader = new parser.SrcReader();

        XmlDocument xmlBefore;

        //Create an xml doc for existing file
        xmlBefore = new XmlDocument();

        //load project
        //xmlBefore.Load("../../../../globalAssets/tests/sample/fullProject.xml");
        xmlBefore.Load("../../../../globalAssets/tests/sample2/timmarcus.xml");

        //this is needed to make xpath queries
        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlBefore.NameTable);
        namespaceManager.AddNamespace("src", "http://www.srcML.org/srcML/src");


        //This is where the good stuff happens
        XmlNode root, classes; //, abstractClasses, interfaces;

        classes = reader.GetClasses(xmlBefore, namespaceManager);

        root = xmlBefore.CreateElement("data");
    }

    //----------------------------------------------------------------------------------------------------------------
    //
    //Methods 
    //
    //----------------------------------------------------------------------------------------------------------------

    public static XmlNode ImportNodes(XmlNodeList list, XmlDocument destinationDoc)
    {
        //you cant really just reference a node from one doc to another so you need to import them first

        //create a export node that stores import
        XmlNode exportNode = destinationDoc.CreateElement("exportNode");

        foreach (XmlNode node in list)
        {
            exportNode.AppendChild(destinationDoc.ImportNode(node, true));
        }
        return exportNode;
    }

    public static XmlNode ImportNode(XmlNode node, XmlDocument destinationDoc)
    {
        //you cant really just reference a node from one doc to another so you need to import them first

        return destinationDoc.ImportNode(node, true);
    }

    public static String XmlToJson(XmlDocument xmlFile)
    {
        return JsonConvert.SerializeXmlNode(xmlFile);
    }

}