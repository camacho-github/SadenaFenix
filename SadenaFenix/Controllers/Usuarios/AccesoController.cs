﻿using SadenaFenix.Models.Usuarios;
using SadenaFenix.Services;
using System.Web.Mvc;


namespace SadenaFenix.Controllers.Usuarios
{

    public class AccesoController : Controller
    {
        /* Properties. */
        private Servicio Servicio;

        // Default constructor
        public AccesoController()
        {
            Servicio = new Servicio();
        }

        public ActionResult Salir()
        {
            return View("~/Views/Usuarios/Acceso/Salir.cshtml");
        }

        // GET: Acceso/Ingresar
        public ActionResult Ingresar()
        {
            return View("~/Views/Usuarios/Acceso/Ingresar.cshtml");
        }

        // POST: Acceso/IniciarSesion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IniciarSesion(Usuario usuario)
        {
            try
            {
                //Servicio.IniciarSesion();
                return RedirectToAction("~/Views/Nacimientos/Consultas/Consultar.cshtml");
            }
            catch
            {
                return View(nameof(Ingresar));
            }
        }

        // GET: Acceso/FinalizarSesion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinalizarSesion(Usuario usuario)
        {
            try
            {
                //Servicio.FinalizarSesion();
                return RedirectToAction("~/Views/Usuarios/Acceso/Salir.cshtml");
            }
            catch
            {
                return View(nameof(Ingresar));
            }
        }

    }
}