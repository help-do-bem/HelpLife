using HelpLife.Models;

namespace HelpLife.ViewModels
{
    public class ChatViewModel
    {
        public IList<Chat> Chats { get; set; }
        public Chat Chat { get; set; }
    }
}