using System;
using System.Xml.Linq;
using System.Threading.Tasks;
using Services;
using System.IO;
using Grpc.Core;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net;

namespace Pipes
{

    //Wierd name, but the class it extends from is a class generted by the proto file/files
    public class ParseService : Services.GameLog.GameLogBase
    {
        //this is the function that is run once the server is called 
        //the parameters type and the name and what it returns are set in the proto generated files Trial.cs & TrialGrpc.cs
        public override Task<JsonReply> MainInteraction(ParsingRequest request, ServerCallContext context)
        {
            // tells us who is calling us
            Console.WriteLine("Being called by " + context.Host.ToString());

            // Initiates variables so the software knows where to get the data 
            string filepath = "../../../../../dit355/globalAssets/inbox/srcML.xml";
            string jsonPath = "../../../../../dit355/globalAssets/outbox/xml2json.json";
            string exepath = System.AppDomain.CurrentDomain.BaseDirectory + "../../../../parser/parser/bin/Debug/parser.exe";
            string result = WebUtility.HtmlDecode(request.Address.ToString());

            //this adds the new xml file and decodes it
            XDocument file = XDocument.Parse(result);
            
            // deletes any files that might exsist from a previous call request
            File.Delete(filepath);
            File.Delete(jsonPath);
            
            //This saves the XML file down in the right location
            file.Save(filepath);

            Process Parsing = new Process();
            try
            {
                // What arguments the file will take when it starts
                Parsing.StartInfo.FileName = exepath;
                Parsing.Start();
                Parsing.WaitForExit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
       
            //Empties the string so no data is left to contaminate the new data set
            result = string.Empty;
            //Reads the produced data from the parse process
            using (StreamReader r = new StreamReader(jsonPath))
            {
                var json = r.ReadToEnd();
                JObject jsonobj = JObject.Parse(json);
                result = jsonobj.ToString();
            }
            return Task.FromResult(new JsonReply { File = result});
        }
    }

    class ParsePipe
    {
        // Specifies the port the Pipe will listen for requests on
        const int Port = 23455;
        public static void Start()
        {
            // gets the specified IP located in a IPconfig txt file for the ParsePipe
            string Host = NodeConfig.getParseIP();
           
            Server server = new Server
            {
        
                //the setup of the services and the Server 
                Services = { Services.GameLog.BindService(new ParseService()) },
                Ports = { { new ServerPort(Host, Port, ServerCredentials.Insecure) } }
            };

            server.Start();

            // This is just here so the program doesnt just shutdown when it gets done
            Console.WriteLine("Server listening on port " + Port + " and Domain is " + Host);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();
            server.ShutdownAsync().Wait();
        }
    }

}

