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
    class Program
    {
        private static String fileType;
        private static readonly HttpClient client = new HttpClient();
        private static String mainFolderLocation;



        public static void Main(string[] projectId)
        {
            //client.DefaultRequestHeaders.Add("PRIVATE-TOKEN" ,"ZqpfJzg-n9-qQNv2z1N2");
            fileType = ".java"; // controlls what type of files it will get.
                                // String url = "dit341/express-template"; //Used for testing

            mainFolderGetter();
            Console.WriteLine(projectId[0].ToString());

            Boolean isGitlab = true;
            if (projectId[1].ToString() == "f")
            {
                isGitlab = false;
            }


            if (isGitlab)
            {
                if (gitTreeRetriever(projectId[0].ToString()))
                {
                    activateParser(projectId[0].ToString());
                }
            }
            else
            {
                if (GitHubGetter.getMainTree(projectId[0].ToString()))
                {
                    activateParser(projectId[0].ToString());
                }
            }
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
                    String dirPath = mainFolderGetter() + "/Middleware/GitGetter/FileStorer/" + projectId;
                    //File.WriteAllText(dirPath+"/"+ name, responseString); // Creates a seperate file for each code
                    File.AppendAllText(dirPath + fileType, responseString + Environment.NewLine);  // Should create a single file with the contents of all the code files.
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
        private static Boolean gitTreeRetriever(String projectId)
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

                Console.WriteLine("response has not been made");
                String responseString;
                if (responseTask.IsCompleted == true) { responseString = responseTask.Result; }
                else { return false; }

                Console.WriteLine("ResponseString has been made");

                List<TreeObject> noPathFolder = makeTreeList(responseString);

                //Makes a directory for this project
                String dirPath = mainFolderGetter() + "/Middleware/Gitgetter/FileStorer/" + projectId;

                System.IO.Directory.CreateDirectory(dirPath);
                File.WriteAllText(dirPath + Program.getFiletype(), "");

                foreach (TreeObject tree in noPathFolder)
                {
                    TreeNavigator(projectId, tree);
                }
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong with HTTP request :Defaultdance");
                Console.WriteLine(e);
                //Write stuff incase the git repository was not found. 
                return false;
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
                //return true; //Added for the testing of enumerator. Remember to uncomment gitFileRetriever and remove this. 
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

        private static void activateParser(String projectId)
        {
            {

                string PathP = mainFolderGetter() + "/parser/parser/bin/Debug/parser.exe";




                String batAddress = System.AppDomain.CurrentDomain.BaseDirectory + ".SrcmlStarter.bat";
                String storageAddress = mainFolderGetter() + "/Middleware/Gitgetter/FileStorer/";
                storageAddress = storageAddress.Replace("/", "\\ ");
                Console.WriteLine(storageAddress);

                Console.WriteLine("Enumerator and srcml beginning");
                // Part that activates srcml

                // Single code file Srcml call
                //String dirPath = globalFolderGetter(4) + "/GitGetter/FileStorer/" + projectId + fileType; // Change for other code files.
                String dirPath = storageAddress + projectId + fileType; // Change for other code files.
                dirPath = dirPath.Replace(" ", String.Empty);

                Console.WriteLine("Here is the dirpath: " + dirPath); ;
                singleFileSrcmlCall(dirPath, projectId, batAddress);


                //End of srcml part. */


                // This part should start the parser but I'm too lazy to test it right now. 

                Process Project = new Process();
                try
                {
                    //so it know where to find the file it should use to start the proccess
                    //if no actual file is specified it will just open the specified folder
                    Project.StartInfo.FileName = PathP;
                    Project.StartInfo.Arguments = dirPath;
                    // What arguments the file will take when it starts
                    Project.Start();
                    Project.WaitForExit();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
        protected static String globalFolderGetter(int backwardsSteps)
        { // Used to move backwards in the folders

            String endlocation = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //Console.WriteLine("This is the exelocation = "+ endlocation);
            for (int i = 0; i < backwardsSteps; i++)
            {
                endlocation = Path.GetDirectoryName(endlocation);
                //Console.WriteLine("This is the endlocation = " + endlocation);
                //Console.WriteLine("Current iteration: " + i);
            }


            return endlocation;
        }

        public static String mainFolderGetter()
        {
            if (mainFolderLocation != null)
            {
                return mainFolderLocation;
            }
            String endlocation = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //Boolean boolean = false;
            while (endlocation.Contains("Middleware")) // Check to make sure that middleware is spelled exactly the same as the middleware folder. CHeck for capital letters etc.
            {
                endlocation = Path.GetDirectoryName(endlocation);
            }
            mainFolderLocation = endlocation;


            return endlocation;
        }

        private static void singleFileSrcmlCall(String dirpath, String projectId, String batAddress)
        {
            Console.WriteLine("Here is the project id: " + projectId);

            Char[] charArray = projectId.ToCharArray();

            String fileName = "";
            for (int i = charArray.Length - 1; i > 0; i--)
            {
                if (charArray[i] == '/')
                {
                    break;
                }
                fileName += charArray[i];
            }
            char[] charArray2 = fileName.ToCharArray();

            fileName = "";

            for (int i = charArray2.Length - 1; i >= 0; i--)
            {
                fileName += charArray2[i];
            }

            // String srcmlAddress = mainFolderGetter() + "/globalAssets/inbox/"+ fileName+".xml";
            String srcmlAddress = mainFolderGetter() + "/globalAssets/inbox/srcML.xml";
            srcmlAddress = srcmlAddress.Replace(" ", "");

            Process srcml = new Process();
            //srcml.StartInfo.FileName = "cmd.exe";
            // srcml.StartInfo.UseShellExecute = false;
            // srcml.StartInfo.RedirectStandardInput = true;
            //srcml.Start();

            //StreamWriter srcmlText = srcml.StandardInput;
            srcml.StartInfo.FileName = batAddress; // IF YOU WANT TO CHANGE WHERE THE OUTPUT FILES GOES THEN CHANGE IT IN THE BAT FILE

            //srcml.StartInfo.Arguments = enumerator.Current+" "+dirPath+ "_srcml/"+ fileName;
            srcml.StartInfo.Arguments = dirpath + " " + srcmlAddress;
            // What arguments the file will take when it starts
            //srcmlText.WriteLine(argu);
            srcml.Start();
            srcml.WaitForExit();

        }

        public static String getFiletype()
        {
            return fileType;
        }

        public static HttpClient getClient()
        {
            return client;
        }




    }
}

