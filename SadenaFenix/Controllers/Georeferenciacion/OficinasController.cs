﻿using Newtonsoft.Json;
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
using System.Globalization;
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

            if (usuario.Rol.RolId == 3)
            {
                ViewBag.perfilInvalido = 1;
            }

            ConsultarOficinasPeticion peticion = new ConsultarOficinasPeticion
            {
                Cabecero = new CabeceroPeticion
                {
                    SesionId = usuario.SesionId
                },

                ColMunicipios = new Collection<Municipio>()
            };
            ConsultarOficinasRespuesta respuesta = new GeoServicio().ConsultarOficinas(peticion);
            respuesta.UserJson = userJson;

            if (! respuesta.Cabecero.EsRespuestaExistosa())
            {
                respuesta.DTOficinas = new DataTable();                
            }

            ViewBag.FechaReporte = DateTime.Now.ToString("dd/MM/yyyy h:mm tt", CultureInfo.InvariantCulture);
            string s = "@IzaSy@2q7-n¡6n79RnZ5Yf7bRPU9tFjz¡cuCn@";            
            s = s.Replace('@', 'A').Replace('¡', 'l');
            ViewBag.ApiKey = s;

            return View(respuesta);
        }

        [HttpGet]
        public ActionResult CrearOficina(string userJson)
        {
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(userJson);
            usuario.Json = userJson;
            ViewBag.UserJson = userJson;

            CabeceroPeticion cabeceroPeticion = new CabeceroPeticion
            {
                SesionId = usuario.SesionId
            };

            Servicio servicio = new Servicio();

            Oficina oficina = new Oficina();
            CatalogoMunicipioRespuesta catalogoMunicipioRespuesta = servicio.ConsultarCatalogoMunicipioGeografia(cabeceroPeticion);
            oficina.MunicipioLista = new List<Municipio>(catalogoMunicipioRespuesta.ColMunicipio);
            
            CatalogoLocalidadRespuesta catalogoLocalidadRespuesta = servicio.ConsultarCatalogoLocalidadGeografiaCoahuila(cabeceroPeticion);
            oficina.LocalidadLista = new List<Localidad>(catalogoLocalidadRespuesta.ColLocalidad);

            oficina.TipoLista = new List<TipoOficina>();
            oficina.TipoLista.Add(new TipoOficina(1, "Oficialia"));
            oficina.TipoLista.Add(new TipoOficina(2, "Módulo Hospitalario"));

            string s = "@IzaSy@2q7-n¡6n79RnZ5Yf7bRPU9tFjz¡cuCn@";
            s = s.Replace('@', 'A').Replace('¡', 'l');
            ViewBag.ApiKey = s;

            return View(oficina);
        }


        [WebMethod]
        public ActionResult GuardarOficina(string jsonOficina)
        {
            InsertarOficinaPeticion peticion = new InsertarOficinaPeticion
            {
                Oficina = JsonConvert.DeserializeObject<Oficina>(jsonOficina)
            };

            GeoServicio geoServicio = new GeoServicio();
            InsertarOficinaRespuesta respuesta = geoServicio.InsertarOficina(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult ActualizarOficina(int id)
        {
            Usuario usuario = new Usuario
            {
                UsuarioDesc = "Administrador",
                CorreoE = "elcorreo@hot.com.mx",
                Rol = new Rol{RolId =  2 }
            };            
            usuario.Json = JsonConvert.SerializeObject(usuario);

            ViewBag.UserJson = usuario.Json;

            CabeceroPeticion cabeceroPeticion = new CabeceroPeticion
            {
                SesionId = 1
            };

            ConsultarOficinaPeticion peticion = new ConsultarOficinaPeticion
            {
                OId = id
            };

            GeoServicio geoServicio = new GeoServicio();
            ConsultarOficinaRespuesta respuesta = geoServicio.ConsultarOficina(peticion);
            Oficina oficina = respuesta.Oficina;

            Servicio servicio = new Servicio();
            CatalogoMunicipioRespuesta catalogoMunicipioRespuesta = servicio.ConsultarCatalogoMunicipioGeografia(cabeceroPeticion);
            oficina.MunicipioLista = new List<Municipio>(catalogoMunicipioRespuesta.ColMunicipio);

            CatalogoLocalidadRespuesta catalogoLocalidadRespuesta = servicio.ConsultarCatalogoLocalidadGeografiaCoahuila(cabeceroPeticion);
            oficina.LocalidadLista = new List<Localidad>(catalogoLocalidadRespuesta.ColLocalidad);

            oficina.TipoLista = new List<TipoOficina>();
            oficina.TipoLista.Add(new TipoOficina(1, "Oficialia"));
            oficina.TipoLista.Add(new TipoOficina(2, "Módulo Hospitalario"));

            string s = "@IzaSy@2q7-n¡6n79RnZ5Yf7bRPU9tFjz¡cuCn@";
            s = s.Replace('@', 'A').Replace('¡', 'l');
            ViewBag.ApiKey = s;

            return View(oficina);
        }

        [WebMethod]
        public ActionResult ActualizarOficina(string jsonOficina)
        {
            ActualizarOficinaPeticion peticion = new ActualizarOficinaPeticion
            {
                Oficina = JsonConvert.DeserializeObject<Oficina>(jsonOficina)
            };

            GeoServicio geoServicio = new GeoServicio();
            ActualizarOficinaRespuesta respuesta = geoServicio.ActualizarOficina(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }


        [WebMethod]
        public ActionResult EliminarOficina(int oid)
        {
            GeoServicio geoServicio = new GeoServicio();
            ActualizarOficinaRespuesta respuesta = geoServicio.EliminarOficina(oid);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

    }
}
