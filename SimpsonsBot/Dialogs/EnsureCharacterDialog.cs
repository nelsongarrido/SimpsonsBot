using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Dialogs
{
    [Serializable]
    public class EnsureCharacterDialog : IDialog<Model.UserProfile>
    {
        Model.UserProfile _profile;

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(AskForCharacter);

            return Task.CompletedTask;
        }

        private Task AskForCharacter(IDialogContext context, IAwaitable<object> result)
        {
            if (!context.UserData.TryGetValue("profile", out _profile))
                _profile = new Model.UserProfile();

            if (string.IsNullOrWhiteSpace(_profile.FavoriteCharacter))
                PromptDialog.Text(context, CharacterEntered, "What is your favorite character?");
            
            return Task.CompletedTask;
        }

        private async Task CharacterEntered(IDialogContext context, IAwaitable<string> result)
        {
            _profile.FavoriteCharacter = await result;
            context.Done(_profile);
        }
    }
}
