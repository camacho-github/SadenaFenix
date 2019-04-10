using Newtonsoft.Json;
using SadenaFenix.Models.Usuarios;
using SadenaFenix.Services;
using SadenaFenix.Transport.Catalogos;
using SadenaFenix.Transport.Nacimientos.Archivos;
using SadenaFenix.Transport.Usuarios.Acceso;
using SadenaFenix.Views.Nacimientos.Configuraciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace SadenaFenix.Controllers.Configuraciones
{
    public class ConfiguracionesController : Controller
    {
        // GET: Configuraciones
        public ActionResult DiasExtemporaneos(string userJson)
        {
            /* Obtener json del usuario. */

            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(userJson);
            usuario.Json = userJson;

            ViewBag.UserJson = userJson;

            CabeceroPeticion peticion = new CabeceroPeticion
            {
                SesionId = usuario.SesionId
            };

            if (usuario.Rol.RolId == 3)
            {
                ViewBag.perfilInvalido = 1;
            }

            Servicio servicio = new Servicio();
            ParametroRespuesta respuesta = servicio.ConsultarParametroRegistroExtemporaneo(peticion);

            Parametros parametros = new Parametros
            {
                NoDiasExtemporaneos = respuesta.ParametroValor
            };

            return View(parametros);
        }

        [WebMethod]
        public ActionResult ActualizarDiasExtemporaneos(int valor)
        {
            ActualizarParametroPeticion peticion = new ActualizarParametroPeticion
            {
                ParametroValor = valor
            };

            Servicio servicio = new Servicio();
            ActualizarParametroRespuesta respuesta = servicio.ActualizarDiasExtemporaneos(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
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
