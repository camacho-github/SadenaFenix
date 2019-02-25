using Newtonsoft.Json;
using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Models.Georeferenciacion;
using SadenaFenix.Models.Usuarios;
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
    public class OficinasController : Controller
    {
        // GET: Oficinas/OficinasConsulta
        [HttpGet]
        public ActionResult OficinasConsulta(string userJson)
        {

            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(userJson);
            usuario.Json = userJson;

            ConsultarOficinasPeticion peticion = new ConsultarOficinasPeticion();
            peticion.Cabecero = new CabeceroPeticion();
            peticion.Cabecero.SesionId = usuario.SesionId;

            peticion.ColMunicipios = new Collection<Municipio>();
            ConsultarOficinasRespuesta respuesta = new GeoServicio().ConsultarOficinas(peticion);
            respuesta.UserJson = userJson;

            if (! respuesta.Cabecero.EsRespuestaExistosa())
            {
                respuesta.DTOficinas = new DataTable();                
            }

            return View(respuesta);
        }

        [HttpGet]
        public ActionResult CrearOficina(string userJson)
        {
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(userJson);
            usuario.Json = userJson;

            CabeceroPeticion cabeceroPeticion = new CabeceroPeticion();
            cabeceroPeticion.SesionId = usuario.SesionId;
            
            Servicio servicio = new Servicio();

            Oficina oficina = new Oficina();
            CatalogoMunicipioRespuesta catalogoMunicipioRespuesta = servicio.ConsultarCatalogoMunicipioGeografia(cabeceroPeticion);
            oficina.MunicipioLista = new List<Municipio>(catalogoMunicipioRespuesta.ColMunicipio);
            
            CatalogoLocalidadRespuesta catalogoLocalidadRespuesta = servicio.ConsultarCatalogoLocalidadGeografiaCoahuila(cabeceroPeticion);
            oficina.LocalidadLista = new List<Localidad>(catalogoLocalidadRespuesta.ColLocalidad);

            oficina.TipoLista = new List<TipoOficina>();
            oficina.TipoLista.Add(new TipoOficina(1, "Oficialia"));
            oficina.TipoLista.Add(new TipoOficina(2, "Módulo Hospitalario"));

            InsertarOficinaPeticion oficinaPeticion = new InsertarOficinaPeticion();
            oficinaPeticion.Oficina = oficina;
            oficinaPeticion.UserJson = userJson;

            return View(oficinaPeticion);
        }


        [WebMethod]
        public ActionResult GuardarOficina(string jsonOficina)
        {
            InsertarOficinaPeticion peticion = new InsertarOficinaPeticion();
            peticion.Oficina = JsonConvert.DeserializeObject<Oficina>(jsonOficina);

            GeoServicio geoServicio = new GeoServicio();
            InsertarOficinaRespuesta respuesta = geoServicio.InsertarOficina(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
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
