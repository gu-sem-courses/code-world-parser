
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Grpc.Core;
using Trial;

namespace TrialClient
{
    class ClientProgram
    {
        public static void Main(string[] args) 
        {
            Channel channel = new Channel("10.0.97.223", 23456, ChannelCredentials.Insecure);
            var client = new Trial.Trial.TrialClient(channel);
            String user = "David";

            var reply = client.MessageExamples(new HelloRequest { Name = user });
            Console.WriteLine("Greeting: " + reply.Message);

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
