using Newtonsoft.Json;
using System;
using System.Xml;
using System.IO;
using System.Diagnostics;

[Serializable]

class Program
{
    protected static void Main(string[] args)
    {

        XmlDocument srcML, gameObjects;

        /*Create an xml doc for existing file*/
        srcML = new XmlDocument();
        
        /*projects*/
        /*
        string[] projects = new string[6];
        projects[0] = @"../../../../globalAssets/tests/official/k9.xml";
        projects[1] = @"../../../../globalAssets/tests/official/bitcoin.xml";
        projects[2] = @"../../../../globalAssets/tests/sample/reuxProject.xml";
        projects[3] = @"../../../../globalAssets/tests/sample/omniProject.xml";
        projects[4] = @"../../../../globalAssets/tests/sample/databaseProject.xml";
        */
        string inbox = @"../../../../globalAssets/inbox/srcML.xml";

        /*set the project here*/
        string project = inbox;

        /*load project(s)*/
        String srcMLPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, project));

        srcML.Load(srcMLPath);

        /*create a namspace for xpath querying*/
        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(srcML.NameTable);
        namespaceManager.AddNamespace("src", "http://www.srcML.org/srcML/src");

        /*retrieve data*/
        parser.SrcMLFilter reader = new parser.SrcMLFilter(); // parser for srcMl
        XmlElement classes = srcML.CreateElement("JavaProject");// node that will store data
        reader.GetClasses(classes, srcML, namespaceManager);//callback that appends data to the "classes" node

        /*import xml info*/
        gameObjects = new XmlDocument();
        XmlNode data = gameObjects.CreateElement("JavaProject");
        data = ImportNode(classes, gameObjects);
        gameObjects.AppendChild(data);

        /*filter the xml in json format*/
        parser.JsonParser jsonReader = new parser.JsonParser(); // custom parser for xml
        String json = jsonReader.XmlToJson(gameObjects); // pops root node out of xml document

        /*place json into outbox*/
        ExportJson(json);
    }


    public static XmlNode ImportNode(XmlNode node, XmlDocument destinationDoc)
    {
        //you cant really just reference a node from one doc to another so you need to import them first
        return destinationDoc.ImportNode(node, true);
    }

    public static void ExportJson(String jsonString)
    {
        try
        {
            string outbox = @"../../../../../dit355/globalAssets/outbox/xml2json.json";
            String path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, outbox));
            File.WriteAllText(path, jsonString);
            
        }
        catch (Exception u)
        {
            Console.WriteLine("error");
            Console.WriteLine(u.ToString());
        }
    }
}