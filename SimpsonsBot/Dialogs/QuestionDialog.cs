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
        Model.Question currentQuestion;

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Ping pong?");
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {

            questionHelper = new Question.QuestionHelper();
            currentQuestion = questionHelper.GetRamdonQuestion();

            var message = await argument;
            if (message.Text == "si")
            {
                PromptDialog.Choice(context,
                    this.OnOptionSelected,
                    currentQuestion.Answers,
                    currentQuestion.Text);
            }
            else
            {
                await context.PostAsync($"Ok, como quieras!");
                context.Done("");
            }
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<Model.Answer> result)
        {
            var msg = await result;

            if (currentQuestion.CorrectAnswer == msg.Number)
            {
                await context.PostAsync($"{msg} correcto");
                context.Done(msg);
            }
            else
            {
                await context.PostAsync($"{msg} incorrecto. A seguir viendo capitulos de los Simpsons");
                context.Done(msg);
            }
        }
    }
}
