using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using System.IO;
using Grpc.Core;
using Newtonsoft.Json.Linq;

namespace Server2
{
    public class ServiceImpl : Services.GameLog.GameLogBase
    {

        public override Task<JsonReply> MainInteraction(ParsingRequest request, ServerCallContext context)
        {
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

    }
    public class ServerProgram
    {
        const string Host = "0.0.0.0";
        const int Port = 23456;

        static void Main(string[] args)
        {
        
            Server server = new Server
            {
                Services = { Services.GameLog.BindService(new ServiceImpl()) },
                Ports = { { new ServerPort(Host, Port, ServerCredentials.Insecure) } }
            };

            server.Start();

            Console.WriteLine("Server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();
            server.ShutdownAsync().Wait();
        }
    }
}
