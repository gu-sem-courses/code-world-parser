using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml;

[Serializable]

class Program
{
    protected static void Main(string[] args)
    {
        //new xml object
        XmlDocument xmlFile = new XmlDocument();
        //load project
        xmlFile.Load("singleClass.xml");
        //set the root ("unit" element)
        XmlNode root = xmlFile.DocumentElement;

        if(root.HasChildNodes){
            Console.WriteLine(root.Name);
            Console.WriteLine("attributes:");
            for (int i = 0; i < root.Attributes.Count; i++){
                Console.WriteLine(root.Attributes[i].Value);
            }

            //add a namespace [this is necessary for the nodes to be selected]
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlFile.NameTable);
            String srcMlNameSpace = "http://www.srcML.org/srcML/src";
            namespaceManager.AddNamespace("", srcMlNameSpace);

            //create an arrayList to store all elements with 'decl_stmt' tags
            XmlNodeList elements= root.SelectNodes("/unit/class");
            foreach (XmlNode element in elements)
            {
                Console.WriteLine(element.Value);
                Console.WriteLine(element.Name);
            }

        }// end of if-state controlling the child

        /***** Will use later *****/
        //try
        //{
        //    //String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "producedJSON/test2.json");
        //    String path = "../../../../assets/xml2json.json";
        //    // serialize JSON to a string and then write string to a file
        //    File.WriteAllText(path, JsonConvert.SerializeObject(xmlFile));
        //    // serialize JSON directly to a file
        //    using (StreamWriter file = File.CreateText(path))
        //    {
        //        JsonSerializer serializer = new JsonSerializer();
        //        serializer.Serialize(file, xmlFile);
        //    }
        //}

        //catch (Exception u)
        //{
        //    Console.WriteLine(u.ToString());
        //}

    }

    public static bool IsFertile(XmlNode node, String name){
        if (node.HasChildNodes)
        {
            Console.WriteLine(name + " has childeren");
            return true;
        }
        else
        {
            Console.WriteLine(name + " has no childeren");
            return false;
        }
    }
}