using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Permissions;

[Serializable]

public class Movie
{
    public string Name { get; set; }
    public int Year { get; set; }
}

class Parsing
    {
    //private const string directoryPath = @"\producedJSON\test2.json";

  
    static void Main(string[] args)
        {

             string xmlString =  System.IO.File.ReadAllText(fileName);  //fix the file name and see what to put instead 
            /* check this website for converting an XML file into a type string
            https://stackoverflow.com/questions/15895670/converting-an-xml-file-to-string-type*/

        Movie movie = new Movie
            {
                Name = "Bad Boys",
                Year = 1995
            };

        try
        {
           //String path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "producedJSON/test2.json");
            String path = "producedJSON/test2.json";
            // serialize JSON to a string and then write string to a file
            File.WriteAllText(path, JsonConvert.SerializeObject(movie));
            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, movie);
            }
        }

        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
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

