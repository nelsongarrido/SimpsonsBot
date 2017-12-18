using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Question
{
    [Serializable]
    public static class CharactersHelper
    {
        public static Model.Character GetCharacter(string character)
        {
            List<Model.Character> questions;
            using (StreamReader r = new StreamReader(@"Data\characters.json"))
            {
                string json = r.ReadToEnd();
                questions = JsonConvert.DeserializeObject<List<Model.Character>>(json);
            }
            return questions.FirstOrDefault(f => f.name == character);
        }
    }
}