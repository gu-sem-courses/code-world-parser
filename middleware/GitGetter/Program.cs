using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitFetcher{

    public class GitGetter{
            private static readonly HttpClient client = new HttpClient();

            public static void Main(){
                gitFileRetriever();
            }
        async public static void gitFileRetriever(/* String projectId, String sha, String privateToken*/){ // This method is meant to take the id/sha of a single file. 
            // String address = "www.gitlab.com/projects/"+ projectId + "/repository/blobs/"+ sha + "/raw?private_token=" + privateToken;

            String address = "https://gitlab.com/dit341/express-template/raw/master/package.json";
            
            // The part where the request is actually made
            try{
                Console.WriteLine("Sending request");
            Task<String> responseTask = client.GetStringAsync(address);
            while(responseTask.IsCompleted!=true){

            }
            String responseString = responseTask.Result;
            //Testing code
            Console.WriteLine("Here is the response: " +responseString);
            //Console.WriteLine(count);
            //End of testing code
            try{
                File.WriteAllText("FileStorer\\tempfile.txt",responseString);
            }catch(Exception e){
                Console.WriteLine("Making file went wrong"); Console.WriteLine(e);
            }

            }catch(Exception e){
                Console.WriteLine("Something went wrong with HTTP request :Defaultdance");
                Console.WriteLine(e);
                //Write stuff incase the git repository was not found. 
            }

            
        }
        }
    }