using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Services.Georeferenciacion;
using SadenaFenix.Transport.Georeferenciacion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SadenaFenix.Controllers.Georeferenciacion
{
    public class GeoOficialiasController : Controller
    {
        // GET: GeoOficialias/OficialiasTabla
        [HttpGet]
        public ActionResult OficialiasTabla()
        {
            ConsultaOficialiaPeticion peticion = new ConsultaOficialiaPeticion();
            peticion.ColMunicipios = new Collection<Municipio>();
            ConsultaOficialiaRespuesta respuesta = new GeoServicio().ConsultarOficialias(peticion);
            if(respuesta.Cabecero.EsRespuestaExistosa())
            {
                return View(respuesta.DTOficialia);
            }
            else
            {
                return View();
            }           
        }

        // GET: GeoOficialias/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GeoOficialias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GeoOficialias/Create
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

        // GET: GeoOficialias/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GeoOficialias/Edit/5
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

        // GET: GeoOficialias/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GeoOficialias/Delete/5
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
