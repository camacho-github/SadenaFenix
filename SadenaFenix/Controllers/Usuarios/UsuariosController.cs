using Newtonsoft.Json;
using SadenaFenix.Models.Nacimientos.Archivos;
using SadenaFenix.Models.Usuarios;
using SadenaFenix.Services;
using SadenaFenix.Transport.Catalogos;
using SadenaFenix.Transport.Nacimientos.Archivos;
using SadenaFenix.Transport.Usuarios.Acceso;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace SadenaFenix.Controllers.Usuarios
{
    public class UsuariosController : Controller
    {
        // GET: Oficinas/OficinasConsulta
        [HttpGet]
        public ActionResult UsuariosConsulta(string userJson)
        {

            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(userJson);
            ViewBag.userJson = userJson;
            
            if (usuario.Rol.RolId > 1)
            {
                SesionPeticion SesionPeticion = new SesionPeticion();
                SesionPeticion.Cabecero.SesionId = usuario.SesionId;

                new Servicio().FinalizarSesion(SesionPeticion);
                return View("~/Views/Usuarios/Acceso/Salir.cshtml");
            }

            ConsultaUsuariosPeticion peticion = new ConsultaUsuariosPeticion
            {
                Cabecero = new CabeceroPeticion()
            };
            peticion.Cabecero.SesionId = usuario.SesionId;

            ConsultarUsuariosRespuesta respuesta = new Servicio().ConsultarUsuarios(peticion);
            
            if (!respuesta.Cabecero.EsRespuestaExistosa())
            {
                respuesta.DTUsuarios = new DataTable();
            }

            return View(respuesta);
        }

        [HttpGet]
        public ActionResult CrearUsuario(string userJson)
        {
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(userJson);
            usuario.Json = userJson;
            ViewBag.UserJson = userJson;

            CabeceroPeticion cabeceroPeticion = new CabeceroPeticion
            {
                SesionId = usuario.SesionId
            };

            Servicio servicio = new Servicio();
            CatalogoRolesRespuesta catalogoRoles = servicio.ConsultarCatalogoRoles(cabeceroPeticion);
            UsuarioAlta nuevoUsuario = new UsuarioAlta
            {
                RolesLista = new List<Rol>(catalogoRoles.ColRoles)
            };

            return View(nuevoUsuario);
        }

        [WebMethod]
        public ActionResult GuardarUsuario(string jsonUsuarioAlta)
        {
            try
            {
                UsuarioAlta alta = new UsuarioAlta();
                alta = JsonConvert.DeserializeObject<UsuarioAlta>(jsonUsuarioAlta);
                InsertarUsuarioPeticion peticion = new InsertarUsuarioPeticion();
                peticion.Usuario = alta;

                Servicio servicio = new Servicio();
                InsertarUsuarioRespuesta respuesta = servicio.InsertarUsuario(peticion);
                return Json(respuesta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                InsertarUsuarioRespuesta respuestaExc = new InsertarUsuarioRespuesta();
                respuestaExc.Cabecero = new CabeceroRespuesta();
                respuestaExc.Cabecero.CodigoRespuesta = 2;
                return Json(respuestaExc, JsonRequestBehavior.AllowGet);                
            }
            
        }


        [HttpGet]
        public ActionResult ActualizarUsuario(string userJson, int id)
        {
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(userJson);            
            usuario.Json = userJson;

            ViewBag.UserJson = usuario.Json;

            ConsultarUsuarioPeticion peticion = new ConsultarUsuarioPeticion();
            peticion.UsuarioId = id;
            peticion.Cabecero = new CabeceroPeticion
            {
                SesionId = usuario.SesionId                
            };

            Servicio servicio = new Servicio();
            CatalogoRolesRespuesta catalogoRoles = servicio.ConsultarCatalogoRoles(peticion.Cabecero);
            
            ConsultarUsuarioRespuesta respuesta = servicio.ConsultarUsuario(peticion);
            respuesta.Usuario.RolesLista = new List<Rol>(catalogoRoles.ColRoles);
            return View(respuesta.Usuario);
        }

        [WebMethod]
        public ActionResult ActualizarUsuario(string jsonUsuarioAlta)
        {
            ActualizarUsuarioPeticion peticion = new ActualizarUsuarioPeticion
            {
                Usuario = JsonConvert.DeserializeObject<UsuarioAlta>(jsonUsuarioAlta)
            };

            Servicio servicio = new Servicio();
            ActualizarUsuarioRespuesta respuesta = servicio.ActualizarUsuario(peticion);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [WebMethod]
        public ActionResult EliminarUsuario(int usuarioId)
        {
            Servicio servicio = new Servicio();
            ActualizarUsuarioRespuesta respuesta = servicio.EliminarUsuario(usuarioId);
            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BitacoraUsuarios(string userJson)
        {
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(userJson);
            ViewBag.userJson = userJson;

            if (usuario.Rol.RolId > 1)
            {
                SesionPeticion SesionPeticion = new SesionPeticion();
                SesionPeticion.Cabecero.SesionId = usuario.SesionId;

                new Servicio().FinalizarSesion(SesionPeticion);
                return View("~/Views/Usuarios/Acceso/Salir.cshtml");
            }

            ConsultaUsuariosPeticion peticion = new ConsultaUsuariosPeticion
            {
                Cabecero = new CabeceroPeticion()
            };
            peticion.Cabecero.SesionId = usuario.SesionId;

            ConsultarUsuariosRespuesta respuesta = new Servicio().ConsultarBitacoraUsuarios(peticion);

            if (!respuesta.Cabecero.EsRespuestaExistosa())
            {
                respuesta.DTUsuarios = new DataTable();
            }

            return View(respuesta);
        }
        

    }
}
