using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace GitGetter2
{
    class Program
    {
    
      
        private static String fileType;
        private static readonly HttpClient client = new HttpClient();



        public static void Main(string[] projectId)
        {
            //client.DefaultRequestHeaders.Add("PRIVATE-TOKEN" ,"ZqpfJzg-n9-qQNv2z1N2");
            fileType = ".js"; // controlls what type of files it will get.
                              // String url = "dit341/express-template"; //Used for testing
            Console.WriteLine(projectId[0].ToString());
            gitTreeRetriever(projectId[0].ToString());
            Console.ReadKey();
        }

        public static bool gitFileRetriever(String projectId, String filepath, String name)
        { // This method is meant to take the id/filepath of a single file. 
          //String address = "https://gitlab.com/"+ projectId +"/raw/master/"+filepath;
            String address = "https://gitlab.com/api/v4/projects/" + WebUtility.UrlEncode(projectId) + "/repository/files/" + WebUtility.UrlEncode(filepath) + "/raw?ref=master";
            Console.WriteLine(address);

            // The part where the request is actually made
            try
            {
                Console.WriteLine("Sending request");
                Task<String> responseTask = client.GetStringAsync(address);
                while (responseTask.IsCompleted == false)
                {
                }
                Console.WriteLine("Request done");
                String responseString = "";
                try
                {
                    responseString = responseTask.Result;
                    Console.WriteLine("Response string has been made");

                }
                catch (AggregateException e)
                {
                    Console.WriteLine(e.Message);
                };

                try
                {
                    File.WriteAllText("../../../../GitGetter2/FileStorer/" + name, responseString);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Making file went wrong"); Console.WriteLine(e);
                    return false;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong with HTTP request :Defaultdance");
                Console.WriteLine(e);
                return false;
                //Write stuff incase the git repository was not found. 
            }

            return true;
        }
        private static void gitTreeRetriever(String projectId)
        {
            //urlEncoder(projectId);
            String address = "https://gitlab.com/api/v4/projects/" + urlEncoder(projectId) + "/repository/tree";
            Console.WriteLine(address);
            // String address = "https://gitlab.com/api/v4/projects/dit341%2Fexpress-template/repository/tree";
            // The part where the request is actually made
            try
            {
                Console.WriteLine("Sending request");
                Task<String> responseTask = client.GetStringAsync(address);
                while (responseTask.IsCompleted != true) { }

                String responseString = responseTask.Result;

                List<TreeObject> noPathFolder = makeTreeList(responseString);

                foreach (TreeObject tree in noPathFolder)
                {
                    TreeNavigator(projectId, tree);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong with HTTP request :Defaultdance");
                Console.WriteLine(e);
                //Write stuff incase the git repository was not found. 
            }
        }

        private static bool gitTreeRetriever(String projectId, String filepath)
        { // intended to be used recursivly to get the things in all the folders
            String address = "https://gitlab.com/api/v4/projects/" + urlEncoder(projectId) + "/repository/tree?path=" + WebUtility.UrlEncode(filepath);
            Console.WriteLine(address);
            // String address = "https://gitlab.com/api/v4/projects/dit341%2Fexpress-template/repository/tree";
            // The part where the request is actually made
            try
            {
                Console.WriteLine("Sending request");
                Task<String> responseTask = client.GetStringAsync(address);
                while (responseTask.IsCompleted != true) { }

                String responseString = responseTask.Result;

                List<TreeObject> noPathFolder = makeTreeList(responseString);

                foreach (TreeObject tree in noPathFolder)
                {
                    TreeNavigator(projectId, tree);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong with HTTP request :Defaultdance");
                Console.WriteLine(e);
                return false;
                //Write stuff incase the git repository was not found. 
            }
            return true;
        }
        private static bool TreeNavigator(String projectId, TreeObject map)
        { // Checks if the tree object is a file or folder and calls the appropriate method. 


            if (map.type == "tree")
            {
                return gitTreeRetriever(projectId, map.path);
            }
            else if (map.type == "blob" && map.name.Contains(fileType))
            {
                return gitFileRetriever(projectId, map.path, map.name);
            }
            else
            {
                return false;
            }
        }
        private static List<TreeObject> makeTreeList(String populationMaker)
        {
            String populationString = populationMaker;
            //Console.WriteLine(populationString);

            List<TreeObject> tempTreeObject = new List<TreeObject>();

            JsonConvert.PopulateObject(populationString, tempTreeObject);

            return tempTreeObject;
        }
        public static String urlEncoder(String url)
        {
            String encoded_url = "";

            char[] letters = url.ToCharArray();

            for (int i = 0; i < letters.Length; i++)
            {
                if (letters[i] == '/')
                {
                    String tempUrl = "";
                    for (int x = 0; x < i; x++)
                    {
                        tempUrl += letters[x];
                    }
                    tempUrl += "%2F";
                    for (int y = i + 1; y < letters.Length; y++)
                    {
                        tempUrl += letters[y];
                    }

                    letters = tempUrl.ToCharArray();
                }

            }

            encoded_url = new string(letters);

            return encoded_url;
        }

    }
}

