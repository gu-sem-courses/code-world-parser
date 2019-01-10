using System;

namespace Middleware
{
    public class Middleware
    {
        static void Main(string[] args)
        {

            /*args = new string[2];
            args[0] = "jorelsin";
            args[1] = "rpg-game-manager";
            UnityNode.Start(args); */
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
