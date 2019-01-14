using System;
using System.IO;
using System.Net.Http;
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

        // Used to report what error has occured to the client.
        private static Boolean errorHasOccured = false;
        private static String errorSpecification = "";



        public static void Main(string[] projectId)
        {

            fileType = ".java"; // controlls what type of files it will retrieve from the database.

            mainFolderGetter();

            // Used to check which database the user wants use to pull from
            Boolean isGitlab = true;
            if (projectId[1].ToString() == "f")
            {
                isGitlab = false;
            }


            if (isGitlab)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                if (GitLabGetter.gitTreeRetriever(projectId[0].ToString()) && !errorHasOccured)
                {
                    PrepareSrcml(projectId[0].ToString());
                }
            }
            else
            {
                if (GitHubGetter.getMainTree(projectId[0].ToString()) && !errorHasOccured)
                {
                    PrepareSrcml(projectId[0].ToString());
                }
            }
        }



        public static String urlEncoder(String url)
        {
            // Sometimes you need to encode your url for it to work properly and this method does that.
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

        private static void PrepareSrcml(String projectId)
        {
            { // This method prepares neccessary paths and data for the Srcml to be called and then calls Srcml with that data.

                string PathP = mainFolderGetter() + "/parser/parser/bin/Debug/parser.exe";



                // The address of the bat file with the commands to start srcml
                String batAddress = System.AppDomain.CurrentDomain.BaseDirectory + ".SrcmlStarter.bat";
                // Where the project file is stored
                String storageAddress = mainFolderGetter() + "/GitFilter/Gitgetter/FileStorer/";
                // If spaces are not removed then bugs can occur so they are removed
                storageAddress = storageAddress.Replace("/", "\\ ");
                Console.WriteLine(storageAddress);

                Console.WriteLine("Enumerator and srcml beginning");
                // Part that activates srcml

                // Single code file Srcml call
                String dirPath = storageAddress + projectId + fileType; // Change for other code files.
                dirPath = dirPath.Replace(" ", String.Empty);

                Console.WriteLine("Here is the dirpath: " + dirPath); ;
                singleFileSrcmlCall(dirPath, projectId, batAddress);
            }

        }
        protected static String globalFolderGetter(int backwardsSteps)
        { // Used to move backwards in the folders

            String endlocation = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            for (int i = 0; i < backwardsSteps; i++)
            {
                endlocation = Path.GetDirectoryName(endlocation);
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
            while (endlocation.Contains("GitFilter")) // Check to make sure that GitFilter is spelled exactly the same as the GitFilter folder. CHeck for capital letters etc.
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

            // Used to get the projectname of the project
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
            
            String srcmlAddress = mainFolderGetter() + "/globalAssets/inbox/srcML.xml";
            srcmlAddress = srcmlAddress.Replace(" ", "");

            Process srcml = new Process();
            try { 

            srcml.StartInfo.FileName = batAddress; // IF YOU WANT TO CHANGE WHERE THE OUTPUT FILES GOES THEN CHANGE IT IN THE BAT FILE
            // What arguments the file will take when it starts
            srcml.StartInfo.Arguments = dirpath + " " + srcmlAddress;
            
            srcml.Start();
            srcml.WaitForExit();
            }
            catch (Exception e)
            {
                if (errorHasOccured == false)
                {
                    errorHasOccured = true;
                    errorSpecification = "Srcml";
                }
                Console.WriteLine(e.Message);

            }

        }

        public static String getFiletype()
        {
            return fileType;
        }

        public static HttpClient getClient()
        {
            return client;
        }

        public static Boolean getErrorBool()
        {
            return errorHasOccured;
        }

        public static void ErrorOccured(String error)
        {
            errorHasOccured = true;
            errorSpecification = error;
        }


    }
}

