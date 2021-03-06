﻿using System.Web.Mvc;
using Newtonsoft.Json;
using SadenaFenix.Facade.Nacimientos.Archivos;
using SadenaFenix.Models.Usuarios;
using SadenaFenix.Transport;
using SadenaFenix.Transport.Nacimientos.Archivos;
using SadenaFenix.Transport.Usuarios.Acceso;

namespace SadenaFenix.Controllers.Nacimientos
{
    public class ArchivosController : Controller
    {
        public ArchivosFacade ArchivosFacade;

        public ArchivosController()
        {
            ArchivosFacade = new ArchivosFacade();
        }

        [HttpPost]
        public ActionResult Importar(ImportarArchivosViewModel viewModel)
        {
            /* Take user. */
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(viewModel.UserJson);
            usuario.Json = viewModel.UserJson;
            viewModel.Usuario = usuario;
            ViewBag.UserJson = viewModel.UserJson;

            /* Saving files */
            CabeceroRespuesta cabeceroRespuesta = ArchivosFacade.SalvarArchivos(viewModel);
            viewModel.CabeceroRespuesta = cabeceroRespuesta;
            return View("~/Views/Nacimientos/Archivos/Importar.cshtml", viewModel);          

        }

        // GET: Archivos
        public ActionResult SeleccionarArchivos(string userJson)
        {
            ImportarArchivosViewModel viewModel = new ImportarArchivosViewModel
            {
                Usuario = new Usuario { Json = userJson }
            };
            ViewBag.UserJson = userJson;

            return View("~/Views/Nacimientos/Archivos/Importar.cshtml", viewModel);
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