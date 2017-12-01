using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using SimpsonsBot.Model;

namespace SimpsonsBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            context.Call(new Dialogs.QuestionDialog(), this.AfterQuestionDialog);
        }

        private async Task AfterQuestionDialog(IDialogContext context, IAwaitable<object> result)
        {
            var ffff = await result;
            var profile = new Model.UserProfile();

            //Si no ingreso su personaje favorito le pregunta
            if (context.UserData.TryGetValue(@"profile", out profile))
                context.Wait(MessageReceivedAsync);
            else
                context.Call(new Dialogs.FavoriteCharacterDialog(), this.AfterFavoriteCharacterDialog);
        }

        private async Task AfterFavoriteCharacterDialog(IDialogContext context, IAwaitable<UserProfile> result)
        {
            var profile = await result;

            context.UserData.SetValue(@"profile", profile);

            await context.PostAsync($"bien {profile.FavoriteCharacter}");

            context.Wait(MessageReceivedAsync);
        }

    }
}