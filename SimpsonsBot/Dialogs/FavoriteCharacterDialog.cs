using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Dialogs
{
    [Serializable]
    public class FavoriteCharacterDialog : IDialog<Model.UserProfile>
    {
        Model.UserProfile _profile = new Model.UserProfile();

        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Cual es tu personaje favorito de los Simpson?");
            context.Wait(AskForCharacter);
        }

        private async Task AskForCharacter(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            //await context.PostAsync($"bien {profile.FavoriteCharacter}");

            var msg = await result;
            _profile.FavoriteCharacter = msg.Text;
            var reply = context.MakeMessage();
            var thumbnailCard = GetProfileThumbnailCard(msg.Text);

            if (thumbnailCard != null)
            {
                reply.AttachmentLayout = Microsoft.Bot.Connector.AttachmentLayoutTypes.Carousel;
                reply.Attachments.Add(thumbnailCard);

                await context.PostAsync(reply);

                context.Done(_profile);
            }
            else
            {
                await context.PostAsync("No tengo ese personaje.");
            }
        }

        private Microsoft.Bot.Connector.Attachment GetProfileThumbnailCard(string character)
        {
            var characterCurrent = Question.CharactersHelper.GetCharacter(character);

            if (characterCurrent != null)
            {
                var thumbnailCard = new Microsoft.Bot.Connector.ThumbnailCard
                {
                    // title of the card  
                    Title = characterCurrent.name,
                    //subtitle of the card  
                    Subtitle = characterCurrent.name,
                    //Detail Text  
                    Text = characterCurrent.name,
                    // smallThumbnailCard  Image  
                    Images = new List<Microsoft.Bot.Connector.CardImage> { new Microsoft.Bot.Connector.CardImage(characterCurrent.icon) },
                };

                return thumbnailCard.ToAttachment();
            }
            else
                return null;
        }
    }
}
