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
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            context.Call<Model.UserProfile>(new Dialogs.EnsureCharacterDialog(), CharacterEnsured);
            return Task.CompletedTask;
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