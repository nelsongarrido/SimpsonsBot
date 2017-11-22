using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Dialogs
{
    [Serializable]
    public class EnsureCharacterDialog : IDialog<Model.UserProfile>
    {
        Model.UserProfile _profile = new Model.UserProfile();

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("What is your favorite character?");
            context.Wait(AskForCharacter);
        }

        private async Task AskForCharacter(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var msg = await result;
            _profile.FavoriteCharacter = msg.Text;
            context.Done(_profile);
        }
    }
}
