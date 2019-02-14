using Newtonsoft.Json;
using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Models.Georeferenciacion;
using SadenaFenix.Services;
using SadenaFenix.Services.Georeferenciacion;
using SadenaFenix.Transport.Catalogos;
using SadenaFenix.Transport.Georeferenciacion;
using SadenaFenix.Transport.Usuarios.Acceso;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

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
                return View(new DataTable());
            }           
        }


        [HttpGet]
        public ActionResult CrearOficialia()
        {
            CabeceroPeticion cabeceroPeticion = new CabeceroPeticion();
            cabeceroPeticion.SesionId = 1;
            Servicio servicio = new Servicio();

            Oficialia oficialia = new Oficialia();
            CatalogoMunicipioRespuesta catalogoMunicipioRespuesta = servicio.ConsultarCatalogoMunicipioGeografia(cabeceroPeticion);
            oficialia.MunicipioLista = new List<Municipio>(catalogoMunicipioRespuesta.ColMunicipio);


            CatalogoLocalidadRespuesta catalogoLocalidadRespuesta = servicio.ConsultarCatalogoLocalidadGeografiaCoahuila(cabeceroPeticion);                       
            oficialia.LocalidadLista = new List<Localidad>(catalogoLocalidadRespuesta.ColLocalidad);
            return View(oficialia);
        }

        [WebMethod]
        public ActionResult GuardarOficialia(string jsonOficialia)
        {
            InsertarOficialiaPeticion peticion = new InsertarOficialiaPeticion();
            peticion.Oficialia = JsonConvert.DeserializeObject<Oficialia>(jsonOficialia);

            GeoServicio geoServicio = new GeoServicio();
            InsertarOficialiaRespuesta respuesta = geoServicio.InsertarOficialia(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ObtenerLocalidadesPorMunicipio()
        {
            
            return PartialView("");
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
