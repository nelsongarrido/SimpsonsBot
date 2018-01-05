using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Model
{

    [Serializable]
    public partial class Quote
    {
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}