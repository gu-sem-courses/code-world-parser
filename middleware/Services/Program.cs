using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Utils;

namespace Trial{

    class Service{

        public static void MessageExample(Trial.ClientProgram client){
            string result = "have you ever seen a chadelier!!!!!?";
            Console.WriteLine("Div Result: " + result);
        }
    }
}