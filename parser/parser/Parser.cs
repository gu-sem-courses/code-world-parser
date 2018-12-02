using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml;

[Serializable]

class Program
{
    protected static void Main(string[] args)
    {
        XmlDocument xmlBefore, xmlAfter;
        //XmlNodeList attr, meth;
        XmlNode name, attributes, methods; // superClasses, subClasses, components, associations; //<-- these are our constraints

        //Create an xml doc for existing file
        xmlBefore = new XmlDocument();
        //load project
        xmlBefore.Load("../../../../assets/tests/sample/singleClass.xml");
        XmlNode root = xmlBefore.DocumentElement;

        //this is needed to make xpath queries
        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlBefore.NameTable);
        namespaceManager.AddNamespace("src", "http://www.srcML.org/srcML/src");

        name = GetClassName(xmlBefore, namespaceManager);
        attributes = GetAttributes(xmlBefore, namespaceManager);
        methods = GetMethods(xmlBefore, namespaceManager);

        xmlAfter = new XmlDocument();
        XmlNode data, className, classAttributes, classMethods;

        className = ImportNode(name, xmlBefore, xmlAfter);
        classAttributes = ImportNode(attributes, xmlBefore, xmlAfter);
        classMethods = ImportNode(methods, xmlBefore, xmlAfter);

        data = xmlAfter.CreateElement("data");
        data.AppendChild(className);
        data.AppendChild(classAttributes);
        data.AppendChild(classMethods);

        xmlAfter.AppendChild(data); Console.WriteLine(xmlAfter.InnerXml);

        /***** Will use later *****/
        try
        {
            String path = "../../../../assets/xml2json.json";
            // serialize JSON to a string and then write string to a file
            File.WriteAllText(path, JsonConvert.SerializeObject(xmlAfter));
            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, xmlAfter);
            }
        }

        catch (Exception u)
        {
            Console.WriteLine(u.ToString());
        }

    }

    //----------------------------------------------------------------------------------------------------------------
    //
    //Methods 
    //
    //----------------------------------------------------------------------------------------------------------------

    public static String XmlToJson(XmlDocument xmlFile)
    {
        return JsonConvert.SerializeXmlNode(xmlFile);
    }

    /*Imports nodes in a list from one document to another */
    public static XmlNode ImportNodes(XmlNodeList list, XmlDocument originDoc, XmlDocument destinationDoc)
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

    public static XmlNode ImportNode(XmlNode node, XmlDocument originDoc, XmlDocument destinationDoc)
    {
        //you cant really just reference a node from one doc to another so you need to import them first

        return destinationDoc.ImportNode(node, true);
    }

    //----------------------------------------------------------------------------------------------------------------
    //
    //WARNING: hard coded teritorry yeeehhaaww
    //the methods below are only reusable for OUR GROUP PROJECT (DIT3**)
    //
    //----------------------------------------------------------------------------------------------------------------

    public static XmlNode GetClassName(XmlDocument xDoc, XmlNamespaceManager nsm)
    {
        XmlNode classNameNode = xDoc.CreateElement("name");
        classNameNode = xDoc.DocumentElement.SelectSingleNode("/src:unit/src:class/src:name", nsm);
        return classNameNode;
    }

    public static XmlNode GetAttributes(XmlDocument xDoc, XmlNamespaceManager nsm)
    {
        XmlNodeList attributeList = xDoc.DocumentElement.SelectNodes("/src:unit/src:class//src:decl_stmt//src:decl", nsm);
        XmlNode attributeNode = xDoc.CreateElement("attributes");

        //we only need type, name;
        foreach (XmlNode node in attributeList)
        {
            String type, name;
            //retrive the nodes we need and store their value
            type = node.SelectSingleNode("./src:type", nsm).InnerText;

            //if there is a final in a type string, remove it
            if (type.Length > 5 && type.Substring(0, 5).Equals("final"))
            {
                type = type.Substring(5, (type.Length - 5));
            }

            name = node.SelectSingleNode("./src:name", nsm).InnerText;

            //create a new element
            XmlElement attribute, typeElement, nameElement;
            typeElement = xDoc.CreateElement("type");
            typeElement.InnerText = type;

            nameElement = xDoc.CreateElement("name");
            nameElement.InnerText = name;

            attribute = xDoc.CreateElement("attribute");
            attribute.AppendChild(nameElement);
            attribute.AppendChild(typeElement);

            //append the child to the original node
            attributeNode.AppendChild(attribute);

        }
        return attributeNode;
    }

    public static XmlNode GetMethods(XmlDocument xDoc, XmlNamespaceManager nsm)
    {
        XmlNodeList methodList = xDoc.DocumentElement.SelectNodes("/src:unit/src:class//src:function", nsm);
        XmlNode methodNode = xDoc.CreateElement("methods");

        //we only need type, name
        foreach (XmlNode node in methodList)
        {
            String type, name;
            //retrive the nodes we need and store their value
            type = node.SelectSingleNode("./src:type/src:name", nsm).InnerText;
            name = node.SelectSingleNode("./src:name", nsm).InnerText;

            //create elements
            XmlElement method, methodName, methodType;
            methodName = xDoc.CreateElement("name");
            methodName.InnerText = name;

            methodType = xDoc.CreateElement("type");
            methodType.InnerText = type;

            method = xDoc.CreateElement("method");
            method.AppendChild(methodName);
            method.AppendChild(methodType);

            //append results
            methodNode.AppendChild(method);
        }
        return methodNode;
    }

    public static XmlNode GetSubClasses(XmlNodeList list, XmlDocument originDoc, XmlDocument destinationDoc)
    {
        return null;
    }

    public static XmlNode GetSuperClasses(XmlNodeList list, XmlDocument originDoc, XmlDocument destinationDoc)
    {
        return null;
    }

    public static XmlNode GetComponents(XmlNodeList list, XmlDocument originDoc, XmlDocument destinationDoc)
    {
        return null;
    }
}