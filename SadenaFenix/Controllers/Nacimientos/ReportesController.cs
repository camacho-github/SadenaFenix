using SadenaFenix.Models.Usuarios;
using System.Web.Mvc;

namespace Sadena.Controllers.Nacimientos
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }

<<<<<<< HEAD
        public ActionResult Reportes()
        {
            return View("~/Views/Nacimientos/Reportes/Reportes.cshtml");
=======
        public ActionResult VerReportes(string userJson)
        {
            Usuario usuario = new Usuario { Json = userJson };
            return View("~/Views/Nacimientos/Reportes/VerReportes.cshtml",usuario);
>>>>>>> 311ffc61be34c1dc2679b1e186561d35cd8ca43b
        }

        // GET: Reportes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reportes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reportes/Create
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

        // GET: Reportes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reportes/Edit/5
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

        // GET: Reportes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reportes/Delete/5
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