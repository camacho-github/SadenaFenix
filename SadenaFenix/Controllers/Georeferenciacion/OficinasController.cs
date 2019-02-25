using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Services.Georeferenciacion;
using SadenaFenix.Transport.Georeferenciacion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SadenaFenix.Controllers.Georeferenciacion
{
    public class OficinasController : Controller
    {
        // GET: Oficinas/OficinasConsulta
        [HttpGet]
        public ActionResult OficinasConsulta()
        {
            ConsultarOficinasPeticion peticion = new ConsultarOficinasPeticion();
            peticion.ColMunicipios = new Collection<Municipio>();
            ConsultarOficinasRespuesta respuesta = new GeoServicio().ConsultarOficinas(peticion);
            if (respuesta.Cabecero.EsRespuestaExistosa())
            {
                return View(respuesta.DTOficinas);
            }
            else
            {
                return View(new DataTable());
            }
        }

        // GET: Oficinas/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Oficinas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Oficinas/Create
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

        // GET: Oficinas/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Oficinas/Edit/5
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

        // GET: Oficinas/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Oficinas/Delete/5
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
