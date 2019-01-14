using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes
{

    //The class functions all deal with getting the ip in the ip config.txt files we have 
    class NodeConfig
    {

        public static string getGetterIP()
        {
            string ip;
            ip = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "IPconfig/GetterIP.txt");
            return ip;
        }

        public static string getParseIP()
        {
            string ip;
            ip = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "IPconfig/ParseIP.txt");
            return ip;
        }

        public static string getVisualizationIP()
        {
            string ip;
            ip = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "IPconfig/VisualizationIP.txt");
            return ip;
        }

    }
}
