using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Dialogs
{
    [Serializable]
    public class QuestionDialog : IDialog<object>
    {
        protected int count = 1;

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Un ping pong?");
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            if (message.Text == "si")
            {
                PromptDialog.Choice(context,
                    this.OnOptionSelected,
                    new List<string>() { "FlightsOption", "HotelsOption" },
                    "Are you looking for a flight or a hotel?", "Not a valid option", 3);
            }
            else
            {
                await context.PostAsync($"{this.count++}: You said {message.Text}");
                context.Wait(MessageReceivedAsync);
            }
        }

        private Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            return Task.CompletedTask;
        }
    }
}
