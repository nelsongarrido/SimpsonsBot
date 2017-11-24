using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Question
{
    [Serializable]
    public class QuestionHelper
    {
        List<Model.Question> questions;

        public QuestionHelper()
        {
            using (StreamReader r = new StreamReader(@"Question\simp.json"))
            {
                string json = r.ReadToEnd();
                questions = JsonConvert.DeserializeObject<List<Model.Question>>(json);
            }
        }

        public Model.Question GetRamdonQuestion()
        {
            return questions.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }
    }
}