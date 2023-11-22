using HelpLife.DataBase;
using HelpLife.Extensions;
using HelpLife.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HelpLife.Controllers
{
    public class UsuarioController : Controller
    {
        private LifeContext _context;

        public UsuarioController(LifeContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }

            var lista = _context.Usuarios
                .Include(y => y.MedicosUsuarios)
                .Include(x => x.Historicos)
                .ThenInclude(z => z.Alerta)
                .ToList();

            return View(lista);
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Vincular(int id)
        {
            var _Usuario = _context.Usuarios
                .Where(x => x.Id == id)
                .Include(y => y.MedicosUsuarios)
                .First();

            var _Medico = _context.Medicos
                .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Include(y => y.MedicosUsuarios)
                .First();

            MedicoUsuario _MedicoUsuario = new MedicoUsuario()
            {
                UsuarioId = id,
                Usuario = _Usuario,
                MedicoId = _Medico.Id,
                Medico = _Medico,
            };

            _Usuario.MedicosUsuarios.Add(_MedicoUsuario);
            _Medico.MedicosUsuarios.Add(_MedicoUsuario);

            _context.Update(_Usuario);
            _context.Update(_Medico);
            
            _context.SaveChanges();

            this.ShowMessage($"Paciente vinculado com Sucesso.", false);

            var lista = _context.Usuarios
                .Include(y => y.MedicosUsuarios)
                .Include(x => x.Historicos)
                .ThenInclude(z => z.Alerta)
                .ToList();

            return View("Index", lista);
        }
    }
}
