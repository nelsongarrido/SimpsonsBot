using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Model
{
    public class Character
    {
        public string name { get; set; }
        public int age { get; set; }
        public List<string> parent { get; set; }
        public string hair { get; set; }
        public bool @checked { get; set; }
        public string icon { get; set; }
        public bool? enabled { get; set; }
    }
}
   