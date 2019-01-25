using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sadena.Controllers.Nacimientos.Archivos
{
    public class ArchivosController : Controller
    {
        // GET: Archivos
        public ActionResult Importar()
        {
            return View("Views/Nacimientos/Archivos/Importar.cshtml");
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
        public ActionResult Create(IFormCollection collection)
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
        public ActionResult Edit(int id, IFormCollection collection)
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
        public ActionResult Delete(int id, IFormCollection collection)
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