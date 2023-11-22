using HelpLife.DataBase;
using HelpLife.Extensions;
using HelpLife.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HelpLife.Controllers
{
    public class AlertaController : Controller
    {
        private LifeContext _context;

        public AlertaController(LifeContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }

            var _Historicos = _context.Historicos
                .Include(x => x.Alerta)
                .Where(y => y.Id == id)
                .First();

            var lista = _Historicos.Alerta;

            return View(lista);
        }

        [HttpGet]
        public IActionResult Register(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }

            Alerta _Alerta = new Alerta()
            {
                HistoricoId = id,
            };

            return View(_Alerta);
        }

        [HttpPost]
        public IActionResult Register([FromForm] Alerta _Alerta)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }

            _Alerta.MedicoId = _context.Medicos
                .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .First()
                .Id;

            _Alerta.DtCriacao = DateTime.Now;
            _Alerta.Id = 0;

            _context.Alertas.Add(_Alerta);
            _context.SaveChanges();

            return RedirectToAction("Register");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var _Alerta = _context.Alertas
                .Where(x => x.Id == id)
                .First();
            return View(_Alerta);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] Alerta _Alerta)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }

            _context.Alertas.Update(_Alerta);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var alerta = _context.Alertas.Where(x => x.Id == id).First();

            _context.Alertas.Remove(alerta);
            _context.SaveChanges();

            this.ShowMessage("Alerta removido!");

            return RedirectToAction("Index", "Home");
        }
    }
}
