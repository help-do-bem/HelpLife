using HelpLife.DataBase;
using HelpLife.Extensions;
using HelpLife.Models;
using HelpLife.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HelpLife.Controllers
{
    public class MedicoController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly LifeContext _context;

        public MedicoController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, LifeContext context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Register(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var _DBUser = await _userManager.FindByIdAsync(id);

                var _Medico = _context.Medicos.Where(x => x.UserId == id).First();

                if (_DBUser == null)
                {
                    this.ShowMessage("Médico não encontrado.", true);
                    return RedirectToAction("Index", "Home");
                }
                var _VMUser = new RegisterUserViewModel
                {
                    Id = _Medico.Id,
                    Nome = _Medico.Nome,
                    DataNasc = _Medico.DataNasc,
                    CRM = _Medico.CRM,
                    UserId = _DBUser.Id,
                    NomeUsuario = _DBUser.UserName,
                    Email = _DBUser.Email,
                    Telefone = _DBUser.PhoneNumber
                };
                return View(_VMUser);
            }
            return View(new RegisterUserViewModel());
        }

        private bool EntityExists(string id)
        {
            return (_userManager.Users.AsNoTracking().Any(u => u.Id == id));
        }

        private static void MapRegisterUserViewModel(RegisterUserViewModel entityOrigin, IdentityUser entityDestination)
        {
            entityDestination.UserName = entityOrigin.NomeUsuario;
            entityDestination.NormalizedUserName = entityOrigin.NomeUsuario.ToUpper().Trim();
            entityDestination.Email = entityOrigin.Email;
            entityDestination.NormalizedEmail = entityOrigin.Email.ToUpper().Trim();
            entityDestination.PhoneNumber = entityOrigin.Telefone;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterUserViewModel _VMUser)
        {            
            ModelState.Remove("Id");
            ModelState.Remove("UserId");

            if (!string.IsNullOrEmpty(_VMUser.UserId))
            {
                var _Medico = _context.Medicos.Where(x => x.UserId == _VMUser.UserId).First();

                _VMUser.Id = _Medico.Id;
                ModelState.Remove("Senha");
                ModelState.Remove("ConfSenha");
            }

            if (ModelState.IsValid)
            {
                if (EntityExists(_VMUser.UserId))
                {
                    var _DBUser = await _userManager.FindByIdAsync(_VMUser.UserId);
                    if ((_VMUser.Email != _DBUser.Email) &&
                        (_userManager.Users.Any(u => u.NormalizedEmail == _VMUser.Email.ToUpper().Trim())))
                    {
                        ModelState.AddModelError("Email", "Já existe algum usuario registrado com esse E-mail.");
                        return View(_VMUser);
                    }
                    MapRegisterUserViewModel(_VMUser, _DBUser);

                    var resultado = await _userManager.UpdateAsync(_DBUser);
                    if (resultado.Succeeded)
                    {
                        this.ShowMessage("Usuário alterado com Sucesso!.");

                        Medico medico = new Medico()
                        {
                            Nome = _VMUser.Nome,
                            DataNasc = (DateTime)_VMUser.DataNasc,
                            CRM = _VMUser.CRM,
                            UserId = _VMUser.UserId,
                        };
                        _context.Medicos.Add(medico);
                        _context.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        this.ShowMessage("Não foi possível alterar o usuário.", true);
                        foreach (var error in resultado.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(_VMUser);
                    }
                }
                else
                {
                    var _DBUser = await _userManager.FindByEmailAsync(_VMUser.Email);
                    if (_DBUser != null)
                    {
                        ModelState.AddModelError("Email", "Já existe algum usuario registrado com esse E-mail.");
                        return View(_DBUser);
                    }

                    _DBUser = new IdentityUser();
                    MapRegisterUserViewModel(_VMUser, _DBUser);

                    var resultado = await _userManager.CreateAsync(_DBUser, _VMUser.Senha);
                    if (resultado.Succeeded)
                    {
                        this.ShowMessage("Usuário cadastrado com sucesso. Use suas credenciais para fazer login no sistema.");

                        var _medico = await _userManager.FindByNameAsync(_VMUser.NomeUsuario);

                        Medico medico = new Medico()
                        {
                            Id = _VMUser.Id,
                            Nome = _VMUser.Nome,
                            DataNasc = (DateTime)_VMUser.DataNasc,
                            CRM = _VMUser.CRM,
                            UserId = _medico.Id                   
                        };
                        _context.Medicos.Update(medico);
                        _context.SaveChanges();

                        if (User.Identity.IsAuthenticated)
                        {
                            return RedirectToAction("Index");
                        }
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        this.ShowMessage("Erro registrando o Usuário.", true);
                        foreach (var error in resultado.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(_VMUser);
                    }
                }
            }
            else
            {
                return View(_VMUser);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginViewModel login)
        {
            ModelState.Remove("ReturnUrl");

            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(login.Usuario, login.Senha, login.Remember, false);
                if (resultado.Succeeded)
                {
                    login.ReturnUrl = login.ReturnUrl ?? "~/";
                    return LocalRedirect(login.ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tentativa de login inválida. Revise seus dados de acesso e tente novamente.");
                    return View(login);
                }
            }
            else
            {
                return View(login);
            }
        }

        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var usuarios = await _userManager.Users.AsNoTracking().ToListAsync();
            return View(usuarios);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                this.ShowMessage("Usuário não informado.", true);
                return RedirectToAction(nameof(Index));
            }

            if (!EntityExists(id))
            {
                this.ShowMessage("Usuário não encontrado.", true);
                return RedirectToAction(nameof(Index));
            }

            var user = await _userManager.FindByIdAsync(id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var _Medico = _context.Medicos.Where(x => x.UserId == id).First();
            if (user != null)
            {
                var resultado = await _userManager.DeleteAsync(user);
                if (resultado.Succeeded)
                {
                    _context.Medicos.Remove(_Medico);
                    this.ShowMessage("Usuário deletado com sucesso.");
                }
                else
                {
                    this.ShowMessage("Não foi possível deletar o Usuario.", true);
                }
                if (User.FindFirstValue(ClaimTypes.NameIdentifier) == id)
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                this.ShowMessage("Usuário não encontrado.", true);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
