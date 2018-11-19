using System;
using System.IO;
using Trial;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Utils;
using Newtonsoft.Json.Linq;

namespace TrialServer
{

public class ServiceImpl : Trial.Trial.TrialBase{
 public override Task<HelloReply> MessageExamples(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = "Hello Darkness My old friend " + request.Name });
        }

 public override Task<JsonReply> MainInteraction(ParsingRequest request, ServerCallContext context)
 {
     string filepath = "../json1.json";
            string result = string.Empty;
            using (StreamReader r = new StreamReader(filepath))
            {
                var json = r.ReadToEnd();
                var jobj = JObject.Parse(json);       
                result = jobj.ToString();
                Console.WriteLine(result);    
                }        


     return Task.FromResult(new JsonReply{File = result});
 }   

}

    public class ServerProgram
    {
        const string Host = "0.0.0.0";
        const int Port = 23456;     
        static void Main(string[] args){

             Server server = new Server{
                Services = { Trial.Trial.BindService(new ServiceImpl())},
                Ports = {{new ServerPort("0.0.0.0", Port, ServerCredentials.Insecure ) }}
            };

            server.Start();

            Console.WriteLine("Server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();
            server.ShutdownAsync().Wait();        
        }
    }
}