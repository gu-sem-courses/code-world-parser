﻿using Newtonsoft.Json;
using System;
using System.Xml;
using System.IO;

[Serializable]

class Program
{
    protected static void Main(string[] args)
    {
        XmlDocument javaProject, gameObjects;

        /*Create an xml doc for existing file*/
        javaProject = new XmlDocument();

        /*load project(s)*/
        //javaProject.Load("../../../../globalAssets/tests/sample/fullProject.xml");
        javaProject.Load("../../../../globalAssets/tests/sample2/timmarcus.xml");

        /*this is needed to make xpath queries*/
        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(javaProject.NameTable);
        namespaceManager.AddNamespace("src", "http://www.srcML.org/srcML/src");


        /*retrieve data*/
        parser.SrcReader reader = new parser.SrcReader(); //srcML reader class

        XmlNode root; //, abstractClasses, interfaces;
        root = javaProject.CreateElement("data");
        root.AppendChild(reader.GetClasses(javaProject, namespaceManager));

        /*import data*/
        gameObjects = new XmlDocument();
        XmlNode data = gameObjects.CreateElement("data");
        data = ImportNode(root, gameObjects);
        gameObjects.AppendChild(data);


        /*send data to outbox*/
        Boolean result = ExportJson(gameObjects);
        Console.WriteLine(Yay(result));
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

    public static Boolean ExportJson(XmlDocument result) {
        try
        {
            String path = "../../../../globalAssets/outbox/xml2json.json";
            // serialize JSON to a string and then write string to a file
            File.WriteAllText(path, JsonConvert.SerializeObject(result));
            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, result);
            }
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