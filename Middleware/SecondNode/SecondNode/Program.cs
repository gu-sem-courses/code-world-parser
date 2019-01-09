using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Middleware;

namespace Middleware
{
    public class Middleware
    {
        static void Main(string[] args)
        {

            GetterNode.ClietRequest();
            if (args.Length > 0)
            {
                UnityNode.Start(args);
            }
            else
            {
                Console.WriteLine("Select the type of node you want to boot up");
                Console.WriteLine("Type GetterNode to start a GetterNode starting");
                Console.WriteLine("Type ParseNode to get a ParseNode starting");
                Console.WriteLine();
                string userInput = Console.ReadLine();

                if (userInput.Contains("GetterNode"))
                {
                    GetterNode.Start();
                }else if (userInput.Contains("ParseNode"))
                {
                    ParseNode.Start();
                }
                else
                {

                }

                
            }

        }
    }
}
