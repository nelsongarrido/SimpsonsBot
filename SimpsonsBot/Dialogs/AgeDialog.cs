using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace SimpsonsBot.Dialogs
{
    [Serializable]
    public class AgeDialog : IDialog<int>
    {
        private string name;

        public AgeDialog(string name)
        {
            this.name = name;
        }

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync($"{this.name}, cuantos años tenes?");
            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            int age;

            if (int.TryParse(message.Text, out age))
            {
                context.Done(age);
            }
            else
            {
                await context.PostAsync("esa no es una edad, jajaaj");
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}
