using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Dialogs
{

    [LuisModel("500f7c62-5376-46c4-8a5c-d757859597f9", "d8a285498d484265a88a1f30686a2670")]
    [Serializable]
    public class LuisBaseDialog<T> : LuisDialog<T>
    {
        public LuisBaseDialog() { }
        public LuisBaseDialog(ILuisService service) : base(service) { }
    }
}
