using HelpLife.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HelpLife.Extensions
{
    public static class ControllerExtensions
    {
        public static void ShowMessage(this Controller @this, string text, bool error = false)
        {
            @this.TempData["message"] = MessageViewModel.Serialize(text, error ? MessageType.Error : MessageType.Information);
        }
    }
}