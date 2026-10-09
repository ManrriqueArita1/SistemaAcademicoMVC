using Microsoft.AspNet.Identity;
using SistemaAcademicoMVC.Data;
using SistemaAcademicoMVC.Models;
using SistemaAcademicoMVC.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace SistemaAcademicoMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly SistemaAcademicoContext db =
            new SistemaAcademicoContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = db.Usuarios
                .FirstOrDefault(u =>
                    u.Correo == model.Correo &&
                    u.Activo);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Correo o contraseña incorrectos.");

                return View(model);
            }

            var hasher = new PasswordHasher();

            var resultado = hasher.VerifyHashedPassword(
                usuario.PasswordHash,
                model.Password
            );

            if (resultado == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Correo o contraseña incorrectos.");

                return View(model);
            }

            FormsAuthentication.SetAuthCookie(usuario.Correo, false);

            return RedirectToAction("Index", "Estudiantes");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(CreateUserViewModel usuario_)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario_);
            }

            var existeCorreo = db.Usuarios
                .Any(u => u.Correo == usuario_.Correo);

            if (existeCorreo)
            {
                ModelState.AddModelError(
                    "Correo",
                    "Ya existe un usuario con este correo."
                );

                return View(usuario_);
            }

            var hasher = new PasswordHasher();

            var usuario = new Usuario
            {
                Nombre = usuario_.Nombre,
                Correo = usuario_.Correo,
                PasswordHash = hasher.HashPassword(usuario_.Password),
                Activo = true
            };

            db.Usuarios.Add(usuario);
            db.SaveChanges();

            return RedirectToAction("Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}