using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Services;

namespace Pipes
{
    class VisualizationPipe
    {
        public static void Start(string[] args)
        {
            // initiating variables 
            string repository = "";
            string UserName;
            string ProjectName;
            string filepath;
            string page;
            string filetype = ".json";
            // Look in the IPSetter file for a explanation of what NodeConfig.getGetter does.
            string IP = NodeConfig.getGetterIP();
            Console.WriteLine("The server is trying to call the server hosting on: " + IP);
            int Port = 23456;

            // Checks if the we succesfully got the correct amount of arguments and then sets the
            // otherwise we produce a error message saying what happend and close the function call
            try
            {
                UserName = args[0];
                ProjectName = args[1];
                page = args[2];
                repository = UserName + "/" + ProjectName + " " + page;
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an error with the input provided.");
                return;
            }
            //This will set up the channel we will use to communicate with the "Server" process
            // the first value refers to the IP of the PC that is running the Server process, the second is what port to send it to
            // and the third is ChannelCredentials that have to match the one of the Server
            Channel channel = new Channel(IP,Port,ChannelCredentials.Insecure);

            // This makes a new client variable referencing the Trial and TrialGrpc files
            var client = new Services.GameLog.GameLogClient(channel);

            //This makes a new json variable and provides a request message, the return message will then be saved in the json var
            Console.WriteLine(IP + " " + Port);
            var json = client.MainInteraction(new ParsingRequest { Address = repository });

            //sets the variable so we always make the file in the same place no matter the PC that runs it
            //the filetype variable at the end decides what kind of file we will make.
            filepath = System.AppDomain.CurrentDomain.BaseDirectory + "../Project" + filetype;

            //This will write the file on the computer
            System.IO.File.WriteAllText(filepath, json.File);
            channel.ShutdownAsync().Wait();
        }
    }
}
