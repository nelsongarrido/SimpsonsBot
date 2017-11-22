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
            await this.SendWelcomeMessageAsync(context);
        }

        private async Task SendWelcomeMessageAsync(IDialogContext context)
        {
            context.Call(new Dialogs.EnsureCharacterDialog(), this.CharacterEnsured);
        }


        private async Task CharacterEnsured(IDialogContext context, IAwaitable<UserProfile> result)
        {
            var profile = await result;

            context.UserData.SetValue(@"profile", profile);

            await context.PostAsync($"bien {profile.FavoriteCharacter}");

            context.Wait(MessageReceivedAsync);
        }

    }
}