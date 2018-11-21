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
    private const string directoryPath = @"\producedJSON\test2.json";

  
    static void Main(string[] args)
        {


            //FileIOPermission f = new FileIOPermission(FileIOPermissionAccess.Read, "C:\\Users\\LOrdBenche\\source\\repos\\parsingTest\\parsingTest\\producedJSON");
           // f.AddPathList(FileIOPermissionAccess.Write | FileIOPermissionAccess.Read, "C:\\Users\\LOrdBenche\\source\\repos\\parsingTest\\parsingTest\\producedJSON");


        Movie movie = new Movie
            {
                Name = "Bad Boys",
                Year = 1995
            };

        try
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory );
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

