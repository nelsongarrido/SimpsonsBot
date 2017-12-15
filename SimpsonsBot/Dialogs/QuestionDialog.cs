using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using SimpsonsBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Dialogs
{
    [Serializable]
    public class QuestionDialog : LuisBaseDialog<object>
    {
        protected int count = 1;
        Model.Question currentQuestion;

        public QuestionDialog(IDialogContext context)
        {
            context.PostAsync("Ping pong?");
        }


        [LuisIntent("GetPositiveAnswer")]
        public async Task PositiveAnswer(IDialogContext context, LuisResult result)
        {
            currentQuestion = Question.QuestionHelper.GetRamdonQuestion();

            PromptDialog.Choice(context,
                  this.OnOptionSelected,
                  currentQuestion.Answers,
                  currentQuestion.Text);
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Ok.");
            context.Done(true);
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
