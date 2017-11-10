using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;

namespace SimpsonsBot.Dialogs
{
    [Serializable]
    public class NameDialog : IDialog<string>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Cual es su nombre?");

            context.Wait(this.MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if(!string.IsNullOrEmpty(message.Text))
            {
                context.Done(message.Text);
            }
            else
            {
                await context.PostAsync("Elegi un nombre como la gente che.");

                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}
