using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using SimpsonsBot.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Dialogs
{
    //[LuisModel("500f7c62-5376-46c4-8a5c-d757859597f9", "d8a285498d484265a88a1f30686a2670")]
    [Serializable]
    public class MainDialog : LuisBaseDialog<object>
    {
        int _saludos = 1;
        int _noneCount = 0;
        Model.UserProfile profile = new Model.UserProfile();

        public MainDialog() { }
        public MainDialog(ILuisService service) : base(service) { }

        [LuisIntent("Greet")]
        public async Task Greet(IDialogContext context, LuisResult result)
        {
            if (_saludos == 1)
                await context.PostAsync("Hola Yo soy un bot. ¿En qué puedo ayudarte?");
            else if (_saludos == 3)
            {
                _saludos = 0;

                await FavoriteCharacterDialog(context);
            }
            else
                await context.PostAsync("Hola");

            _saludos++;
        }

        [LuisIntent("Laugh")]
        public async Task Laugh(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Que risa!!");
        }

        [LuisIntent("GetGame")]
        public async Task GetGame(IDialogContext context, LuisResult result)
        {
            context.Call(new Dialogs.QuestionDialog(context), this.AfterQuestionDialog);
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            if (_noneCount % 2 == 0)
                await FavoriteCharacterDialog(context);
            else
            {
                context.UserData.TryGetValue(@"profile", out profile);
                var frase = Quotes.QuotesHelper.GetRamdonQuote(profile?.FavoriteCharacter);

                if (frase != null)
                    await context.PostAsync(frase.Text);
                else
                    await context.PostAsync("jaja");

                context.Wait(MessageReceived);
            }

            _noneCount++;
        }

        #region Private Methods
        private async Task FavoriteCharacterDialog(IDialogContext context)
        {
            //Si no ingreso su personaje favorito le pregunta
            if (context.UserData.TryGetValue(@"profile", out profile))
                await context.PostAsync($"Aguante {profile.FavoriteCharacter}.");
            else
                context.Call(new Dialogs.FavoriteCharacterDialog(), this.AfterFavoriteCharacterDialog);
        }

        private async Task AfterQuestionDialog(IDialogContext context, IAwaitable<object> result)
        {
            var ffff = await result;
            var profile = new Model.UserProfile();

            ////Si no ingreso su personaje favorito le pregunta
            //if (context.UserData.TryGetValue(@"profile", out profile))
            //    context.Wait(MessageReceivedAsync);
            //else
            //    context.Call(new Dialogs.FavoriteCharacterDialog(), this.AfterFavoriteCharacterDialog);
        }

        private async Task AfterFavoriteCharacterDialog(IDialogContext context, IAwaitable<UserProfile> result)
        {
            var profile = await result;
            context.UserData.SetValue(@"profile", profile);
            ////await context.PostAsync($"bien {profile.FavoriteCharacter}");

            //var reply = context.MakeMessage();

            //reply.AttachmentLayout = Microsoft.Bot.Connector.AttachmentLayoutTypes.Carousel;
            //reply.Attachments.Add(GetProfileThumbnailCard());

            //await context.PostAsync(reply);
            await context.PostAsync($"bien {profile.FavoriteCharacter}");
        }

        //private Microsoft.Bot.Connector.Attachment GetProfileThumbnailCard()
        //{
        //    var thumbnailCard = new Microsoft.Bot.Connector.ThumbnailCard
        //    {
        //        // title of the card  
        //        Title = "Homer",
        //        //subtitle of the card  
        //        Subtitle = "Homero",
        //        //Detail Text  
        //        Text = "Homero Simpson",
        //        // smallThumbnailCard  Image  
        //        Images = new List<Microsoft.Bot.Connector.CardImage> { new Microsoft.Bot.Connector.CardImage("https://blogdefrases.com/wp-content/uploads/2016/02/La-mejor-frases-de-Homero-Simpson.jpg") },
        //    };

        //    return thumbnailCard.ToAttachment();
        //}
        #endregion
    }
}
