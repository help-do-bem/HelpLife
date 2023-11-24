using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Firestore;
using HelpLife.DataBase;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using HelpLife.Models;
using HelpLife.ViewModels;
using Google.Cloud.Firestore.V1;

namespace HelpLife.Controllers
{
    public class ChatController : Controller
    {
        private LifeContext _context;

        public ChatController(LifeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IList<Chat> chats = new List<Chat>();
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            FirestoreDb db = FirestoreDb.Create("chathelplife");
            CollectionReference collection = db.Collection("chats");
            DocumentReference document = collection.Document();

            var _Medico = _context.Medicos
                .Include(z => z.User)
                .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .First();            

            //await document.SetAsync(new { _id = document.Id, createdAt = Timestamp.GetCurrentTimestamp(), text = "Só pode criticar coreano", user = new { _id = _Medico.User.Email, avatar = "https://i.pravatar.cc/300", tipo = "medico" } });

            FirestoreClient firestoreClient = FirestoreClient.Create();
            DocumentSnapshot snapshot = await document.GetSnapshotAsync();

            Dictionary<string, object> data = snapshot.ToDictionary();

            Query query = collection;
            QuerySnapshot querySnapshot = await query.OrderBy("createdAt").GetSnapshotAsync();
            
            foreach (DocumentSnapshot queryResult in querySnapshot.Documents)
            {
                string nome = string.Empty;
                DateTime createdAt = DateTime.SpecifyKind(queryResult.GetValue<Timestamp>("createdAt").ToDateTime(), DateTimeKind.Unspecified);

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
                    createdAt = TimeZoneInfo.ConvertTimeToUtc(createdAt, timeZoneInfo),
                };
                chats.Add(chat);
            }

            return View(chats);
        }
    }
}
