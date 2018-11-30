using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using System.IO;
using Grpc.Core;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Server2
{
    //Wierd name, but the class it extends from is a class generted by the proto file/files
    public class ServiceImpl : Services.GameLog.GameLogBase
    {

        //this is the function that we have the client process call, 
        //the parameters type and the name and what it returns are set in the proto generated files Trial.cs & TrialGrpc.cs
        public override Task<JsonReply> MainInteraction(ParsingRequest request, ServerCallContext context)
        {

            GetProject(request);
            string filepath = "../../../json1.json";
            string result = string.Empty;
            using (StreamReader r = new StreamReader(filepath))
            {
                var json = r.ReadToEnd();
                JObject jsonobj = JObject.Parse(json);
                result = jsonobj.ToString();
            }
            return Task.FromResult(new JsonReply { File = result });
        }


        // This function starts a the GitGetter process and passes along the request message 
        // from the client proccess as a argument while starting the process
        public void GetProject(ParsingRequest req)
        {
            string PathP = System.AppDomain.CurrentDomain.BaseDirectory + "../../../../GitGetter2/GitGetter2/bin/Debug/Gitgetter2.exe";
           
            Process Project = new Process();
            try
            {
                //so it know where to find the file it should use to start the proccess
                //if no actuall file is specified it will just open the specified folder
                Project.StartInfo.FileName = PathP;
                // What arguments the file will take when it starts
                Project.StartInfo.Arguments = req.Address.ToString();
                Project.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }

   
    // This class contain the "Static void Main" in wich it starts the "Server" and gives it a set of services and a port to listen to.
    public class ServerProgram
    {
        //the host IP, 0.0.0.0 (127.0.0.1 seems to work just as well) means it hosts it locally, 
        //not sure if you can change to another computers IP
        // The Port 23456 is the specific port the proccess will listen on for requests. 
        // the port needs to be the same on the Server and Client proccesses.
        const string Host = "0.0.0.0";
        const int Port = 23456;

        static void Main(string[] args)
        {
        
            Server server = new Server
            {
                //the services or functions that the Server can peform, I guess we can add more if we need to.
                Services = { Services.GameLog.BindService(new ServiceImpl()) },
                Ports = { { new ServerPort(Host, Port, ServerCredentials.Insecure) } }
            };

            server.Start();

            // This is just here so the program doesnt just shutdown when it gets done, could be nice to add 
            // quality of life messages here but can´t think of any right now
            Console.WriteLine("Server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();
            server.ShutdownAsync().Wait();
        }


    }
}
