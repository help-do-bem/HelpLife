using HelpLife.DataBase;
using HelpLife.Extensions;
using HelpLife.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        [HttpGet]
        public IActionResult Index(string filtro)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }

            var lista = _context.Usuarios
                .Where(x => x.Nome.Contains(filtro) || filtro == null)
                .Include(y => y.MedicosUsuarios)
                .Include(x => x.Historicos)
                .ThenInclude(z => z.Alerta)
                .ToList();

            if(filtro != null)
                this.ShowMessage($"Paciente filtrado com sucesso.", false);

            return View(lista);
        }

        [HttpGet]
        public IActionResult IndexMedico()
        {
            IList<Usuario> Usuarios = new List<Usuario>();

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }

            var lista = _context.Usuarios
                .Include(y => y.MedicosUsuarios)
                .Include(x => x.Historicos)
                .ThenInclude(z => z.Alerta)
                .ToList();

            var _Medico = _context.Medicos
                .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .First();

            foreach (var item in lista)
            {
                if (item.MedicosUsuarios != null && item.MedicosUsuarios.Count > 0)
                {
                    if (item.MedicosUsuarios.Where(x => x.UsuarioId == item.Id && x.MedicoId == _Medico.Id).First() != null)
                    {
                        Usuarios.Add(item);
                    }
                }
            }

            return View("Index", Usuarios);
        }

        public IActionResult Detail(int id)
        {
            var Usuario =_context.Usuarios
                            .Where(z => z.Id == id)
                            .Include(x => x.Endereco)
                            .First();

            return View(Usuario);
        }

        public IActionResult Endereco(int id)
        {
            var Usuario = _context.Usuarios
                            .Where(z => z.Id == id)
                            .Include(x => x.Endereco)
                            .First();

            return View(Usuario.Endereco);
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
