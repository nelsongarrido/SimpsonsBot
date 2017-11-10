using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpsonsBot.Dialogs
{
    //[Serializable]
    //public class EnsureProfile : IDialog<Model.UserProfile>
    //{
    //    Model.UserProfile _profile;

    //    public Task StartAsync(IDialogContext context)
    //    {
    //        EnsureProfileName(context);
    //        return Task.CompletedTask;
    //    }

    //    private void EnsureProfileName(IDialogContext context)
    //    {
    //        if (! context.UserData.TryGetValue(@"profile", out _profile))
    //        {
    //            _profile = new Model.UserProfile();
    //        }

    //        if (string.IsNullOrWhiteSpace(_profile.Name))
    //            PromptDialog.Text(context, NameEntered, "Su Nombre");
    //        else
    //            EnsureCompanyName();
    //    }

      //  private void EnsureCompanyName()
      //  {
      //// if(string.IsNullOrWhiteSpace(_profile.CompanyName))
      //  }

      //  private Task NameEntered(IDialogContext context, IAwaitable<string> result)
      //  {
           
      //  }
   // }
}
