using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipes
{
    class NodeConfig
    {

        public static string getGetterIP()
        {
            string ip;
            ip = File.ReadAllText("IPconfig/GetterIP.txt");
            return ip;
        }

        public static string getParseIP()
        {
            string ip;
            ip = File.ReadAllText("IPconfig/ParseIP.txt");
            return ip;
        }

        public static string getVisualizationIP()
        {
            string ip;
            ip = File.ReadAllText("IPconfig/VisualizationIP.txt");
            return ip;
        }

    }
}
