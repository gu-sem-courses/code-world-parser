
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
            Testing();
        }
        private static void Testing(){
            string filepath = "../json2.json";
            Channel channel = new Channel("127.0.0.1", 23456, ChannelCredentials.Insecure);
            var client = new Trial.Trial.TrialClient(channel);
           
            var json = client.MainInteraction(new ParsingRequest {Address ="Pancake"});
            System.IO.File.WriteAllText(filepath, json.File);

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
