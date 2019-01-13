using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Reflection;

namespace GitGetter2
{
    class GitLabGetter
    {

        private static readonly HttpClient client = Program.getClient();
        private static Boolean errorHasOccured = false;
        private static String errorSpecification = "";

        private static bool gitFileRetriever(String projectId, String filepath, String name)
        { // This method is meant to take the id/filepath of a single file. 
          //String address = "https://gitlab.com/"+ projectId +"/raw/master/"+filepath;
            String address = "https://gitlab.com/api/v4/projects/" + WebUtility.UrlEncode(projectId) + "/repository/files/" + WebUtility.UrlEncode(filepath) + "/raw?ref=master";
            Console.WriteLine(address);

            // The part where the request is actually made
            try
            {
                Console.WriteLine("Sending request");
                Task<String> responseTask;
                responseTask = client.GetStringAsync(address);

                while (responseTask.IsCompleted == false) { }

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
                    String dirPath = Program.mainFolderGetter() + "/GitFilter/GitGetter/FileStorer/" + projectId;
                    //File.WriteAllText(dirPath+"/"+ name, responseString); // Creates a seperate file for each code
                    File.AppendAllText(dirPath + Program.getFiletype(), responseString + Environment.NewLine);  // Should create a single file with the contents of all the code files.
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
        public static Boolean gitTreeRetriever(String projectId)
        {
            //urlEncoder(projectId);
            String address = "https://gitlab.com/api/v4/projects/" + Program.urlEncoder(projectId) + "/repository/tree";
            Console.WriteLine("Tree request: " + address);
            // String address = "https://gitlab.com/api/v4/projects/dit341%2Fexpress-template/repository/tree";
            // The part where the request is actually made
            try
            {
                Console.WriteLine("Sending request");
                Task<String> responseTask = client.GetStringAsync(address);
                while (responseTask.IsCompleted != true) { }

                Console.WriteLine("response has not been made");
                String responseString;
                if (responseTask.IsCompleted == true) { responseString = responseTask.Result; }
                else { return false; }

                Console.WriteLine("ResponseString has been made");

                List<GitLabObject> noPathFolder = makeGitLabList(responseString);

                //Makes a directory for this project
                String dirPath = Program.mainFolderGetter() + "/GitFilter/Gitgetter/FileStorer/" + projectId;

                System.IO.Directory.CreateDirectory(dirPath);
                File.WriteAllText(dirPath + Program.getFiletype(), "");

                foreach (GitLabObject tree in noPathFolder)
                {
                    TreeNavigator(projectId, tree);
                }
                return true;

            }
            catch (HttpRequestException e)
            {
                if (errorHasOccured == false)
                {
                    errorHasOccured = true;
                    errorSpecification = "Https: " + e.HResult;
                }
                Console.WriteLine("Something went wrong with HTTP request :Defaultdance");
                Console.WriteLine(e);
                //Write stuff incase the git repository was not found. 
                return false;
            }
        }

        private static bool gitTreeRetriever(String projectId, String filepath)
        { // intended to be used recursivly to get the things in all the folders
            String address = "https://gitlab.com/api/v4/projects/" + Program.urlEncoder(projectId) + "/repository/tree?path=" + WebUtility.UrlEncode(filepath);
            Console.WriteLine(address);
            // String address = "https://gitlab.com/api/v4/projects/dit341%2Fexpress-template/repository/tree";
            // The part where the request is actually made
            try
            {
                Console.WriteLine("Sending request");
                Task<String> responseTask = client.GetStringAsync(address);
                while (responseTask.IsCompleted != true) { }

                String responseString = responseTask.Result;

                List<GitLabObject> noPathFolder = makeGitLabList(responseString);

                foreach (GitLabObject tree in noPathFolder)
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
        private static bool TreeNavigator(String projectId, GitLabObject map)
        { // Checks if the tree object is a file or folder and calls the appropriate method. 


            if (map.type == "tree")
            {
                return gitTreeRetriever(projectId, map.path);
            }
            else if (map.type == "blob" && map.name.Contains(Program.getFiletype()))
            {
                return gitFileRetriever(projectId, map.path, map.name);
                //return true; //Added for the testing of enumerator. Remember to uncomment gitFileRetriever and remove this. 
            }
            else
            {
                return false;
            }

        }
        private static List<GitLabObject> makeGitLabList(String populationMaker)
        {
            String populationString = populationMaker;
            //Console.WriteLine(populationString);

            List<GitLabObject> tempTreeObject = new List<GitLabObject>();

            JsonConvert.PopulateObject(populationString, tempTreeObject);

            return tempTreeObject;
        }


    }
}
