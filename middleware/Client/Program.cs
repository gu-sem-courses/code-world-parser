
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Grpc.Core;
using Trial;
using TrialServer;

namespace TrialClient
{
    class ClientProgram
    {
       
        public static void Main(string[] args)
        {
            Channel channel = new Channel("127.0.0.1", 23456, ChannelCredentials.Insecure);
            var client = new Trial.Trial.TrialClient(channel);
           
            channel.ShutdownAsync().Wait();
        }
    }
}
