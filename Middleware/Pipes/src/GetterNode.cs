using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using System.Threading.Tasks;
using Services;
using Grpc.Core;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;

namespace Pipes
{ //Wierd name, but the class it extends from is a class generted by the proto file/files
    public class GetterService : Services.GameLog.GameLogBase
    {
        
        //this is the function that we have the client process call, 
        //the parameters type and the name and what it returns are set in the proto generated files Trial.cs & TrialGrpc.cs
        public override Task<JsonReply> MainInteraction(ParsingRequest request, ServerCallContext context)
        {
            // initiating variables and tells the pipe were it can find the data
            string filepathXML = "../../../../../dit355/globalAssets/inbox/srcML.xml";
            string result;
            // in case the software has been run before it will delete the file before anything else
            File.Delete(filepathXML);

            // prompt so we can see who is calling us
            Console.WriteLine("Being called by " + context.Host.ToString());
            
            // Starts the chain of operations to have the request handeld
            result = GetProject(request, filepathXML);
            JObject jsonobj = JObject.Parse(result);
            result = jsonobj.ToString();
            return Task.FromResult(new JsonReply { File = result });
        }

        // This function starts a the GitGetter process and then sends the data produced by the GitGetter
        // to the ParsePipe
        public string GetProject(ParsingRequest req, string path)
        {
            // Sets up the process call to the
            string PathP = System.AppDomain.CurrentDomain.BaseDirectory + "../../../../GitFilter/GitGetter/bin/Debug/Gitgetter.exe";
            Process GitGetter = new Process();
            try
            {
                // What arguments the process will take when it starts
                GitGetter.StartInfo.FileName = PathP;
                GitGetter.StartInfo.Arguments = req.Address.ToString();
                // Starts the process and then waits for the process to close down
                GitGetter.Start();
                GitGetter.WaitForExit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return GetterPipe.ClietRequest(path);
        }
    }

    // This class contain the "Static void Main" in wich it starts the "Server" and gives it a set of services and a port to listen to.
    public class GetterPipe
    {
        // The Port 23456 is the specific port the Pipe will listen on for requests. 
        const int Port = 23456;
       
        public static void Start()
        {
            //gets the IP stated of the GetterPipe in the IP config file
            string Host = NodeConfig.getGetterIP();
            
            Server server = new Server
            {
                // the general setup of ther server side of the pipe, the services that the Server can peform
                // and the Host ip and port it will be listning in on
                Services = { Services.GameLog.BindService(new GetterService()) },
                Ports = { { new ServerPort(Host, Port, ServerCredentials.Insecure) } }
                
            };

            server.Start();

            // Tells the user that is was sucessful setting up the server 
            Console.WriteLine("Server listening on port " + Port + " and Domain is " +  Host);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();
            server.ShutdownAsync().Wait();
        }
    
        public static string ClietRequest(string filepathXML)
        {
            // initiates and starts the channel to the ParsePipe
            string IP = NodeConfig.getParseIP();
            int Port = 23455;
            Channel parsepipe = new Channel(IP, Port, ChannelCredentials.Insecure);
            var request = new Services.GameLog.GameLogClient(parsepipe);

            // loads the data to be sent to the through the parse pipe
            string data = string.Empty;
            XDocument file = XDocument.Load(filepathXML);
            data = file.ToString();
           
            // starts the request and then returns the reply data
            var json = request.MainInteraction(new ParsingRequest { Address = WebUtility.HtmlEncode(data)});
            parsepipe.ShutdownAsync().Wait();
            return json.File.ToString();
        }
    }

    
}
