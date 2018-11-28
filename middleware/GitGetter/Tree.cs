using System;
using System.Collections.Generic;

namespace GitGetter_classes{

    class TreeObject{ // You are intended to use newtonsoft.json to populate this using the results of the tree requests to gitlab. 
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string path { get; set; }
        public string mode { get; set; }

        TreeObject(){
        }
    }

}