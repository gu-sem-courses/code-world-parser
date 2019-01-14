using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;

namespace GitGetter2
{
    class GitLabGetter
    {

        private static readonly HttpClient client = Program.getClient();
        private static Boolean errorHasOccured = false;
        private static String errorSpecification = "";

        private static bool gitFileRetriever(String projectId, String filepath, String name)
        { // This method is meant to take the id/filepath of a single file. 
            String address = "https://gitlab.com/api/v4/projects/" + WebUtility.UrlEncode(projectId) + "/repository/files/" + WebUtility.UrlEncode(filepath) + "/raw?ref=master";
            Console.WriteLine(address);

            // The part where the request is actually made
            try
            {
                Console.WriteLine("Sending request");
                Task<String> responseTask;
                responseTask = client.GetStringAsync(address);
                // Waits for the request to finish
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
                    // Makes a file with the contents of the downloaded content or append the contents of the downloaded file to the file in filestorer
                    String dirPath = Program.mainFolderGetter() + "/GitFilter/GitGetter/FileStorer/" + projectId;
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
            String address = "https://gitlab.com/api/v4/projects/" + Program.urlEncoder(projectId) + "/repository/tree";
            Console.WriteLine("Tree request: " + address);
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

                // Makes a list of the objects gitlab returned to us so that you get request the contents of those folders
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
            // The part where the request is actually made
            try
            {
                Console.WriteLine("Sending request");
                Task<String> responseTask = client.GetStringAsync(address);
                while (responseTask.IsCompleted != true) { }

                String responseString = responseTask.Result;

                // Makes a list of the objects gitlab returned to us so that you get request the contents of those folders
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
                // If the thing is a folder
                return gitTreeRetriever(projectId, map.path);
            }
            else if (map.type == "blob" && map.name.Contains(Program.getFiletype()))
            {
                // If the thing is a file
                return gitFileRetriever(projectId, map.path, map.name);
            }
            else
            {
                return false;
            }

        }
        private static List<GitLabObject> makeGitLabList(String populationMaker)
        {// Makes a list of GitLabObjects based on a JSON representation of the contents of a repository folder. 
            String populationString = populationMaker;

            List<GitLabObject> tempTreeObject = new List<GitLabObject>();

            JsonConvert.PopulateObject(populationString, tempTreeObject);

            return tempTreeObject;
        }


    }
}
