using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Question
{
    [Serializable]
    public static class QuestionHelper
    {
        public static Model.Question GetRamdonQuestion()
        {
            List<Model.Question> questions;
            using (StreamReader r = new StreamReader(@"Data\simp.json"))
            {
                string json = r.ReadToEnd();
                questions = JsonConvert.DeserializeObject<List<Model.Question>>(json);
            }
            return questions.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }
    }
}