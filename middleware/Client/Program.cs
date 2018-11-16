
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Grpc.Core;

namespace Trial
{
    class ClientProgram
    {
        private Channel channel;

        public ClientProgram(Channel channel)
        {
            this.channel = channel;
        }

        public static void Main(string[] args)
        {
            var channel = new Channel("127.0.0.1", 23456, ChannelCredentials.Insecure);
            Trial.ClientProgram client = new Trial.ClientProgram(channel);
           
            channel.ShutdownAsync().Wait();
        }
    }
}
