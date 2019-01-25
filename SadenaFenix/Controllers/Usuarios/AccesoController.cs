using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sadena.Models.Usuarios;

namespace Sadena.Controllers.Usuarios
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Ingresar()
        {
            return View("Views/Usuarios/Acceso/Ingresar.cshtml");
        }

        // GET: Acceso
        public ActionResult Salir()
        {
            return View("Views/Usuarios/Acceso/Salir.cshtml");
        }

        // POST: Acceso/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind("CorreoE, Contrasenia")] Usuario usuario)
        {

            return View("Views/Nacimientos/Consultas/Consultar.cshtml");
        }

        // GET: Acceso/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Acceso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Acceso/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Ingresar));
            }
            catch
            {
                return View();
            }
        }

        // GET: Acceso/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Acceso/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Ingresar));
            }
            catch
            {
                return View();
            }
        }

        // GET: Acceso/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Acceso/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Ingresar));
            }
            catch
            {
                return View();
            }
        }
    }
}