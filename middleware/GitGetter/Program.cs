using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitFetcher{

    public class GitGetter{
            private static readonly HttpClient client = new HttpClient();

            public static void Main(){
                gitTreeRetriever("");
            }

             public static String gitFileRetriever( String projectId, String repository, String branch , String filepath){ // This method is meant to take the id/filepath of a single file. 
             String address = "www.gitlab.com/"+ projectId + "/"+ repository +"/raw/"+ branch +"/"+filepath;

            // String address = "https://gitlab.com/dit341/express-template/raw/master/package.json"; Example address
            
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

            return "GitGetter\\FileStorer\\tempfile.txt";
        }
             private static void gitTreeRetriever(String projectId){
                 String address = "https://gitlab.com/api/v4/projects/dit341%2Fexpress-template/repository/tree";  //"https://gitlab.com/dit341/express-template/tree/master";
                // The part where the request is actually made
                try{
                    Console.WriteLine("Sending request");
                    Task<String> responseTask = client.GetStringAsync(address);
                    while(responseTask.IsCompleted!=true){}

                String responseString = responseTask.Result;
                //Testing code
                Console.WriteLine("Here is the response: " +responseString);
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