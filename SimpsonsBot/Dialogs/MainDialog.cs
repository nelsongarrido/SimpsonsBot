using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using SimpsonsBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Dialogs
{
    [LuisModel("500f7c62-5376-46c4-8a5c-d757859597f9", "d8a285498d484265a88a1f30686a2670")]
    [Serializable]
    public class MainDialog : LuisDialog<object>
    {
        int _saludos = 1;

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
                // await context.PostAsync("Esta charla se esta tornando aburrida.");
                // var ffff = await result;
                var profile = new Model.UserProfile();

                //Si no ingreso su personaje favorito le pregunta
                if (context.UserData.TryGetValue(@"profile", out profile))
                    await context.PostAsync("Esta charla se esta tornando aburrida.");
                else
                    context.Call(new Dialogs.FavoriteCharacterDialog(), this.AfterFavoriteCharacterDialog);
            }
            else
                await context.PostAsync("Hola");

            _saludos++;
        }

        [LuisIntent("Ask")]
        public async Task Ask(IDialogContext context, LuisResult result)
        {
            // QnaMakerRespose(context);
            // await context.PostAsync("Usted me esta preguntando");
        }

        [LuisIntent("Laugh")]
        public async Task Laugh(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Que risa!!");
            //context.Wait(MessageReceived);
            //context.Call(new Dialogs.QuestionDialog(), this.AfterQuestionDialog);
        }

        [LuisIntent("Play")]
        public async Task Play(IDialogContext context, LuisResult result)
        {
            //context.Wait(MessageReceived);
            context.Call(new Dialogs.QuestionDialog(), this.AfterQuestionDialog);
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("No se che.");
            context.Wait(MessageReceived);
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

            await context.PostAsync($"bien {profile.FavoriteCharacter}");

            //context.Wait(MessageReceivedAsync);
        }
    }
}
