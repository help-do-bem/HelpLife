using HelpLife.DataBase;
using HelpLife.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpLife.Controllers
{
    public class HistoricoController : Controller
    {
        private LifeContext _context;

        public HistoricoController(LifeContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }

            var _Usuario = _context.Usuarios
                .Include(x => x.Historicos)
                .ThenInclude(y => y.Alerta)
                .Where(z => z.Id == id)
                .First();

            this.ShowMessage($"Mostrando relátorios do paciente {_Usuario.Nome}.", false);

            var lista = _Usuario.Historicos;

            return View(lista);
        }
    }
}
