using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Model
{
    [Serializable]
    public class UserProfile
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
    }
}
