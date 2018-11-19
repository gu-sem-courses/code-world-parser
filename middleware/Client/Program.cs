
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
            //Testing();
            //GetParsedCode()

        }
        private static void Testing(){
            Console.WriteLine("Before Channel");
            Channel channel = new Channel("192.168.43.224", 23456, ChannelCredentials.Insecure);
            var client = new Trial.Trial.TrialClient(channel);
            Console.WriteLine("After Channel");
            String user = "Naief";

            var reply = client.MessageExamples(new HelloRequest { Name = user });
            Console.WriteLine("Greeting: " + reply.Message);

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        private static void GetParsedCode(){

            Console.WriteLine("Enter gitlab address of repository");
            String gitlabURL = Console.ReadLine();
            // Server request with gitlab address
            Channel channel = new Channel("192.168.43.224", 23456, ChannelCredentials.Insecure);
            var client = new Trial.Trial.TrialClient(channel);
            var reply = client.MessageExamples(new HelloRequest { GitlabAdress = gitlabURL });
            
            // Handle the reply. Turn it into a Json file.


            // Give the file to unity. 
        }
    }
        

}
