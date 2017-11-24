using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Model
{
    [Serializable]
    public class Question
    {
        [JsonProperty("answers")]
        public Answer[] Answers { get; set; }

        [JsonProperty("correctAnswer")]
        public long CorrectAnswer { get; set; }

        [JsonProperty("difficulty")]
        public long Difficulty { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
