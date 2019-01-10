using System;
using System.Xml.Linq;
using System.Threading.Tasks;
using Services;
using System.IO;
using Grpc.Core;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Middleware
{

    //Wierd name, but the class it extends from is a class generted by the proto file/files
    public class ParseService : Services.GameLog.GameLogBase
    {
        //this is the function that is run once the server is called 
        //the parameters type and the name and what it returns are set in the proto generated files Trial.cs & TrialGrpc.cs
        public override Task<JsonReply> MainInteraction(ParsingRequest request, ServerCallContext context)
        {

            //this adds the new xml file
            var result = request;
            string filepath = "../../../../../dit355/globalAssets/inbox/srcML.xml";
            string exepath = " ../../../../../parser/parser/bin/Debug/parser.exe;";
            System.IO.File.WriteAllText( filepath, result.ToString());
            Console.WriteLine(exepath);
            Console.WriteLine(filepath);
            // add or copy parsing starting logic
            Process Parsing = new Process();
            try
            {
                //so it know where to find the file it should use to start the proccess
                //if no actual file is specified it will just open the specified folder
                Parsing.StartInfo.FileName = exepath;
                // What arguments the file will take when it starts
                Parsing.Start();
                Parsing.WaitForExit();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Task.FromResult(new JsonReply { File = result.ToString()});
        }
    }

    class ParseNode
    {
        //the host IP, 0.0.0.0 (127.0.0.1 seems to work just as well) means it hosts it locally, 
        //not sure if you can change to another computers IP
        // The Port 23456 is the specific port the proccess will listen on for requests. 
        // the port needs to be the same on the Server and Client proccesses.
        const string Host = "0.0.0.0";
        const int Port = 23455;
        public static void Start()
        { 
            Server server = new Server
            {
                //the services or functions that the Server can peform, I guess we can add more if we need to.
                Services = { Services.GameLog.BindService(new ParseService()) },
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

