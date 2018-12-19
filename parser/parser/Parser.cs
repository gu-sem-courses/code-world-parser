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
        /*Start benchmark*/
        Stopwatch benchmark = new Stopwatch();
        benchmark.Start();

        XmlDocument srcML, gameObjects;

        /*Create an xml doc for existing file*/
        srcML = new XmlDocument();

        /*load project(s)*/
        //srcML.Load("../../../../globalAssets/tests/sample/fullProject.xml");
        String srcMLPath = AppDomain.CurrentDomain.BaseDirectory + "/../../../../../globalAssets/inbox/srcML.xml";
        srcML.Load(srcMLPath);

        /*this is needed to make xpath queries*/
        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(srcML.NameTable);
        namespaceManager.AddNamespace("src", "http://www.srcML.org/srcML/src");


        /*retrieve data*/
        parser.SrcMLReader reader = new parser.SrcMLReader(); //srcML reader class
        XmlElement[] jClasses = reader.GetClasses(srcML, namespaceManager);

        XmlElement classes = srcML.CreateElement("JavaProject");

        foreach(XmlElement jClass in jClasses) {
            classes.AppendChild(jClass);
        }

        /*import xml info*/
        gameObjects = new XmlDocument();
        XmlNode data = gameObjects.CreateElement("JavaProject");
        data = ImportNode(classes, gameObjects);
        gameObjects.AppendChild(data);

        /*filter the xml in json format*/
        parser.JsonReader jsonReader = new parser.JsonReader();
        String json = jsonReader.readSrcML(gameObjects);

        /*place json into outbox*/
        Boolean result = ExportJson(json);
        //Console.WriteLine(Yay(result));

        /*send data to outbox*/
        Boolean result = ExportJson(gameObjects);
        Console.ReadKey();
        /*Close Benchmark*/
        benchmark.Stop();
        Console.WriteLine(Yay(result));
        Console.WriteLine("Benchmark: " + benchmark.Elapsed);
    }

    //----------------------------------------------------------------------------------------------------------------
    //
    //Methods 
    //
    //----------------------------------------------------------------------------------------------------------------

    public static XmlNode ImportNode(XmlNode node, XmlDocument destinationDoc)
    {
        //you cant really just reference a node from one doc to another so you need to import them first

        return destinationDoc.ImportNode(node, true);
    }

    public static String XmlToJson(XmlDocument xmlFile)
    {
        return JsonConvert.SerializeXmlNode(xmlFile);
    }

    public static Boolean ExportJson(String jsonString) {
        try
        {
            String path = System.AppDomain.CurrentDomain.BaseDirectory + "/../../../../globalAssets/outbox/xml2json.json";
            Console.WriteLine(path);
            Console.ReadKey();
            // serialize JSON to a string and then write string to a file
            File.WriteAllText(path, jsonString);
            // serialize JSON directly to a file
            //using (StreamWriter file = File.CreateText(path))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    serializer.Serialize(file, jsonString);
            //}
        }
        catch (Exception u)
        {
            Console.WriteLine(u.ToString());
            return false;
        }
        return true;
    }

    public static String Yay(Boolean reality)
    {
        String res;
        if (reality) {
            res = "────────────────────────────────────────\n────────────────────────────────────────\n───────────████──███────────────────────\n──────────█████─████────────────────────\n────────███───███───████──███───────────\n────────███───███───██████████──────────\n────────███─────███───████──██──────────\n─────────████───████───███──██──────────\n──────────███─────██────██──██──────────\n──────██████████────██──██──██──────────\n─────████████████───██──██──██──────────\n────███────────███──██──██──██──────────\n────███─████───███──██──██──██──────────\n───████─█████───██──██──██──██──────────\n───██████───██──────██──────██──────────\n─████████───██──────██─────███──────────\n─██────██───██─────────────███──────────\n─██─────███─██─────────────███──────────\n─████───██████─────────────███──────────\n───██───█████──────────────███──────────\n────███──███───────────────███──────────\n────███────────────────────███──────────\n────███───────────────────███───────────\n─────████────────────────███────────────\n──────███────────────────███────────────\n────────███─────────────███─────────────\n────────████────────────██──────────────\n──────────███───────────██──────────────\n──────────████████████████──────────────\n──────────████████████████──────────────\n────────────────────────────────────────\n────────────────────────────────────────";
        }
        else {
            res = "░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n░░░░░░░░░░░░░░░░▄▄███▄▄▄░▄▄██▄░░░░░░░\n░░░░░░░░░██▀███████████████▀▀▄█░░░░░░\n░░░░░░░░█▀▄▀▀▄██████████████▄░░█░░░░░\n░░░░░░░█▀▀░▄██████████████▄█▀░░▀▄░░░░\n░░░░░▄▀░░░▀▀▄████████████████▄░░░█░░░\n░░░░░▀░░░░▄███▀░░███▄████░████░░░░▀▄░\n░░░▄▀░░░░▄████░░▀▀░▀░░░░░░██░▀▄░░░░▀▄\n░▄▀░░░░░▄▀▀██▀░░░░░▄░░▀▄░░██░░░▀▄░░░░\n█░░░░░█▀░░░██▄░░░░░▀▀█▀░░░█░░░░░░█░░░\n█░░░▄▀░░░░░░██░░░░░▀██▀░░█▀▄░░░░░░▀▀▀\n▀▀▀▀░▄▄▄▄▄▄▀▀░█░░░░░░░░░▄█░░█▀▀▀▀▀█░░\n░░░░█░░░▀▀░░░░░░▀▄░░░▄▄██░░░█░░░░░▀▄░\n░░░░█░░░░░░░░░░░░█▄▀▀▀▀▀█░░░█░░░░░░█░\n░░░░▀░░░░░░░░░░░░░▀░░░░▀░░░░▀░░░░░░░░\n░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░";
        }
        return res;
    }
}