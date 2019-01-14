using System;

namespace Pipes
{
    public class PipesMain
    {
        static void Main(string[] args)
        {
          //This part is just for testing without manualy giving arguments 
          
            /*
            args = new string[2];
            args[0] = "jorelsin";
            args[1] = "rpg-game-manager";
            UnityNode.Start(args);*/

            // it checks the length of the provided args, if the length is 3 or larger we assume that we are making a
            // starting the pipes for Visualization
            //Otherwise we promtp the user to decide by typing if the wanna set up a Parse Pipe or a Getter Pipe
            if (args.Length >= 3)
            {
               VisualizationPipe.Start(args);
            }
            else
            {
                Console.WriteLine("Select the type of node you want to boot up");
                Console.WriteLine("Type GetterPipe to setup the GetterPipe starting");
                Console.WriteLine("Type ParsePipe to setup the ParsePipe starting");
                Console.WriteLine();
                string userInput = Console.ReadLine();

                if (userInput.Contains("GetterPipe"))
                {
                    GetterPipe.Start();
                }else if (userInput.Contains("ParsePipe"))
                {
                    ParsePipe.Start();
                }
                else
                {

                }
           
                
            }

        }
    }
}
