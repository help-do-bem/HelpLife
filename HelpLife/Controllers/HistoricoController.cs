using HelpLife.DataBase;
using HelpLife.Extensions;
using HelpLife.Models;
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

        public IActionResult Index(int id, DateTime? filtroData)
        {
            IList<Historico> _Historicos = null;
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }

            var _Usuario = _context.Usuarios
                .Include(x => x.Historicos)
                .ThenInclude(y => y.Alerta)
                .Where(z => z.Id == id)
                .First();

            if (filtroData != null)
            {
                _Historicos = _Usuario.Historicos.Where(x => DateTime.Parse(x.DataMedicao.ToString("d")) == filtroData).ToList();
                this.ShowMessage($"Relátorios filtrandos com sucesso.", false);
            }
            else
            {
                _Historicos = _Usuario.Historicos;
                this.ShowMessage($"Mostrando relátorios do paciente {_Usuario.Nome}.", false);
            }

            return View(_Historicos);
        }
    }
}
