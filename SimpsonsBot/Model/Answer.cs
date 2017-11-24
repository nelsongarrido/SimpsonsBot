using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Model
{
    public class Answer
    {
        [JsonProperty("number")]
        public long Number { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
