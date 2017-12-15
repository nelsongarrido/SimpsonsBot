using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Quotes
{
    [Serializable]
    public static class QuotesHelper
    {
        public static Model.Quote GetRamdonQuote()
        {
            List<Model.Quote> quotes;
            using (StreamReader r = new StreamReader(@"Quotes\quote.json"))
            {
                string json = r.ReadToEnd();
                quotes = JsonConvert.DeserializeObject<List<Model.Quote>>(json);
            }
            return  quotes.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
        }
    }
}
