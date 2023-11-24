using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Firestore;
using HelpLife.DataBase;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using HelpLife.Models;
using HelpLife.ViewModels;

namespace HelpLife.Controllers
{
    public class ChatController : Controller
    {
        private LifeContext _context;
        private FirestoreDb _db;
        private CollectionReference _collection;

        public ChatController(LifeContext context)
        {
            _context = context;
            _db = FirestoreDb.Create("chathelplife");
            _collection = _db.Collection("chats");
        }

        public async Task<IActionResult> Index()
        {
            IList<Chat> chats = new List<Chat>();
            ChatViewModel chatViewModel = new ChatViewModel();
            chatViewModel.Chats = chats;

            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");                    

            Query query = _collection;
            QuerySnapshot querySnapshot = await query.OrderBy("createdAt").GetSnapshotAsync();

            foreach (DocumentSnapshot queryResult in querySnapshot.Documents)
            {
                string nome = string.Empty;
                DateTime _createdAt = DateTime.SpecifyKind(queryResult.GetValue<Timestamp>("createdAt").ToDateTime(), DateTimeKind.Unspecified);

                if (queryResult.GetValue<string>("user.tipo") == "medico")
                {
                    nome = _context.Medicos
                        .Include(x => x.User)
                        .Where(x => x.User.Email == queryResult.GetValue<string>("user._id"))
                        .First()
                        .Nome;
                }
                else
                {
                    nome = _context.Usuarios
                        .Where(x => x.Email == queryResult.GetValue<string>("user._id"))
                        .First()
                        .Nome;
                }

                Chat chat = new Chat()
                {
                    Tipo = queryResult.GetValue<string>("user.tipo"),
                    Texto = queryResult.GetValue<string>("text"),
                    Avatar = queryResult.GetValue<string>("user.avatar"),
                    Email = queryResult.GetValue<string>("user._id"),
                    Nome = nome,
                    createdAt = _createdAt.ToLocalTime(),
                };
                chatViewModel.Chats.Add(chat);
                
            }

            return View(chatViewModel);
        }

        public async Task<IActionResult> Register(ChatViewModel chatViewModel)
        {
            FirestoreDb db = FirestoreDb.Create("chathelplife");
            CollectionReference collection = db.Collection("chats");

            DocumentReference document = collection.Document();

            var _Medico = _context.Medicos
                .Include(z => z.User)
                .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .First();

            await document.SetAsync(new { _id = document.Id, createdAt = Timestamp.GetCurrentTimestamp(), text = chatViewModel.Chat.Texto, user = new { _id = _Medico.User.Email, avatar = "https://i.pravatar.cc/300", tipo = "medico" } });

            return RedirectToAction("Index");
        }
    }
}
