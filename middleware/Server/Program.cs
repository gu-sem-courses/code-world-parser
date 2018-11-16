using System;
using System.Runtime.InteropServices;
using System.Threading;
using Grpc.Core;

namespace Trial
{
    class ServerProgram
    {
        const string Host = "0.0.0.0";
        const int Port = 23456;     
        static void Main(string[] args)
        {
             Server server = new Server
            {
                Services = { Trial.BindService(new Trial.Service())},
                Ports = { { Host, Port, ServerCredentials.Insecure } }
            };
            server.Start();

            Console.WriteLine("MathServer listening on port " + Port);

            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();        }
    }
}