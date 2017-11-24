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
        Question.QuestionHelper questionHelper;

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Un ping pong?");
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {

            questionHelper = new Question.QuestionHelper();
            var question = questionHelper.GetRamdonQuestion();

            var message = await argument;
            if (message.Text == "si")
            {
                PromptDialog.Choice(context,
                    this.OnOptionSelected,
                    question.Answers,
                    question.Text);
            }
            else
            {
                await context.PostAsync($"{this.count++}: You said {message.Text}");
                context.Wait(MessageReceivedAsync);
            }
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<object> result)
        {
            var msg = await result;
            await context.PostAsync($"{msg} dsfdsdsfsfsd");
            context.Done(msg);
        }
    }
}
