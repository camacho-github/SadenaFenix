using SadenaFenix.Models.Usuarios;
using SadenaFenix.Views.Nacimientos.Configuraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SadenaFenix.Controllers.Configuraciones
{
    public class ConfiguracionesController : Controller
    {
        // GET: Configuraciones
        public ActionResult DiasExtemporaneos(string userJson)
        {
            /* Obtener json del usuario. */
            Usuario usuario = new Usuario { Json = userJson };
            ViewBag.UserJson = userJson;

            Parametros parametros = new Parametros();
            parametros.NoDiasExtemporaneos = 60;

            return View(parametros);
        }

        // GET: Configuraciones/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Configuraciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Configuraciones/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Configuraciones/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Configuraciones/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Configuraciones/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Configuraciones/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
