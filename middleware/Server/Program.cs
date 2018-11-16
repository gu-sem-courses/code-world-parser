using System;
using Trial;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Utils;

namespace TrialServer
{

public class ServiceImpl : Trial.Trial.TrialBase{
 public override Task<HelloReply> MessageExamples(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = "Hello My Sweet " + request.Name });
        }
}

    public class ServerProgram
    {
        const string Host = "0.0.0.0";
        const int Port = 23456;     
        static void Main(string[] args){

             Server server = new Server{
                Services = { Trial.Trial.BindService(new ServiceImpl())},
                Ports = { { Host, Port, ServerCredentials.Insecure } }
            };

            server.Start();

            Console.WriteLine("MathServer listening on port " + Port);

            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();
            server.ShutdownAsync().Wait();        
        }
    }
}