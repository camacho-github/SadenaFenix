using System.Collections.ObjectModel;
using System.Web.Mvc;
using SadenaFenix.Models.Nacimientos.Archivos;
using SadenaFenix.Models.Usuarios;
using SadenaFenix.Services;
using SadenaFenix.Transport.Nacimientos.Archivos;
using SadenaFenix.Transport.Nacimientos.Consultas;
using SadenaFenix.Transport.Usuarios.Acceso;

namespace SadenaFenix.Controllers.Nacimientos
{
    public class ArchivosController : Controller
    {
        // GET: Archivos
        public ActionResult Importar(string userJson)
        {

            Usuario usuario = new Usuario { Json = userJson };
                                  
            
            return View("~/Views/Nacimientos/Archivos/Importar.cshtml", usuario);
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Archivos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Archivos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Archivos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Archivos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Archivos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Archivos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Archivos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}