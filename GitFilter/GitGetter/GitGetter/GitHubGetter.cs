using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
namespace GitGetter2
{
    class GitHubGetter
    {

        private static readonly HttpClient client = Program.getClient();
        private static Boolean errorHasOccured = false;
        private static String errorSpecification = "";
        private static String access_token = "46ee005df418450c69a336f11ea60cd4e71bff90";


        public static Boolean getMainTree(String projectId)
        {
            String address = "https://api.github.com/repos/" + projectId + "/contents";

            //String address = "https://api.github.com/repos/GokuMohandas/practicalAI/contents"; // Testing address
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            Console.WriteLine(address);

            // The part where the request is actually made
            try
            {
                Console.WriteLine("Sending request. V2");
                client.DefaultRequestHeaders.Add("User-Agent", "C# App");
                client.DefaultRequestHeaders.Add("Authorization", "token "+access_token.ToString());

                // Used to check our current rate limit on github. Not that useful but could be highly useful in the future.
                /*if (!githubAuthent())
                {
                    return false;
                } */  // Currently only used to check if authentication is working without running out the rate.
                
                // THe thing that makes the request to github
                HttpResponseMessage response = client.GetAsync(address).GetAwaiter().GetResult() ;
                
                Console.WriteLine("response has not been made");
                String responseString;
                // The part that takes the response as a string.
                if (response!=null) { responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult(); }
                else { return false; }

                Console.WriteLine("ResponseString has been made");
                Console.WriteLine("Resposne here: "+ responseString);
                // Makes a list of the objects that github returned to us.
                List<HubObject> noPathFolder = makeHubList(responseString);

                //Makes a directory for this project
                String dirPath = Program.mainFolderGetter() + "/GitFilter/Gitgetter/FileStorer/" + projectId;
                
                // Creates the directory which stores the project file
                System.IO.Directory.CreateDirectory(dirPath);
                File.WriteAllText (dirPath + Program.getFiletype(), "");

                foreach (HubObject tree in noPathFolder)
                {
                    FolderNavigator(projectId, tree);
                }
                return true;

            }
            catch (Exception e)
            {
                if (errorHasOccured == false)
                {
                    errorHasOccured = true;
                    errorSpecification = "Http";
                }
                Console.WriteLine("Something went wrong with HTTP request :Defaultdance");
                Console.WriteLine(e);
                //Write stuff incase the git repository was not found. 
                return false;
            }



        }

        private static Boolean getDirTree(String url, String projectId)
        {
            String address = url; // It is possible that I'll have to encode it or parts of it for the address to work. NOT TESTED YET.
            Console.WriteLine(address);
            // The part where the request is actually made
            try
            {
                Console.WriteLine("Sending request");
                // The request is made
                Task<String> responseTask = client.GetStringAsync(address);
                while (responseTask.IsCompleted != true) { }

                String responseString = responseTask.Result;

                // Makes a list of the objects in the response
                List<HubObject> noPathFolder = makeHubList(responseString);

                // Makes a request for each of the objects in the list
                foreach (HubObject tree in noPathFolder)
                {
                    FolderNavigator(projectId, tree);
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

        private static Boolean getFile(String downloadUrl, String projectId)
        {
            String address = downloadUrl; // It is possible that I'll have to encode it or parts of it for the address to work. NOT TESTED YET.
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
                    // Set ups the path to where we put the file containing the repositorys code.
                    String dirPath = Program.mainFolderGetter() + "/GitFilter/GitGetter/FileStorer/" + projectId;
                    dirPath = dirPath.Replace(" ", "");

                    Console.WriteLine("Here is where the file should be: "+ dirPath);
                    File.AppendAllText(dirPath + Program.getFiletype(), responseString +Environment.NewLine);  // Should create a single file with the contents of all the code files.
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

        private static Boolean FolderNavigator(String projectID, HubObject hubObject)
        {// This is used to activate the correct method depending on the type of the object. If the object is a file of the correct type
         //then we download its contents. If it is a folder then we make a request to get a json representation of its contents.
            if (hubObject.type == "dir")
            { // If the object is a folder and potentially has stuff in it
                return getDirTree(hubObject.url, projectID);

            }
            else if (hubObject.type == "file" && hubObject.name.Contains(Program.getFiletype()))
            { // If the object is a actual file
                return getFile(hubObject.download_url, projectID);
            }
            else{
                return false;
            }



        } 


        private static List<HubObject> makeHubList(String populationMaker)
        { // Creates a list of GitHubObjects. Each representing one thing in the Json object we get from github.
            String populationString = populationMaker;

            List<HubObject> tempHubObject = new List<HubObject>();

            JsonConvert.PopulateObject(populationString, tempHubObject);

            return tempHubObject;

        }

        private static Boolean githubAuthent()
        {
            // This method exists only for the sake of the developers. So that they can check how many requests we are currently allowed to do 
            // Could be used for error messaging in the future but that has not been implemented yet.
            String address = "https://api.github.com/users/Anotinas";

            HttpResponseMessage response = client.GetAsync(address).GetAwaiter().GetResult();
            String responseString = "";

            if (response != null) { responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult(); }
            else { return false; }

            String resHeaders = response.Headers.ToString();
            Console.WriteLine("Here are the headers: " + resHeaders);

            return false;
        }

    }
}
