using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitGetter2
{
    public class TreeObject
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string path { get; set; }
        public string mode { get; set; }


        public TreeObject()
        {
        }
    }
}
