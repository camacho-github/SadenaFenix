﻿using SadenaFenix.Models.Nacimientos.Archivos;
using SadenaFenix.Models.Usuarios;
using SadenaFenix.Services;
using SadenaFenix.Transport.Nacimientos.Archivos;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
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

        public ActionResult Salir(Usuario usuario)
        {
            try
            {
                SesionPeticion peticion = new SesionPeticion();
                peticion.Cabecero.SesionId = usuario.SesionId;
                
                Servicio servicio = new Servicio();
                SesionRespuesta respuesta = servicio.FinalizarSesion(peticion);
                return View("~/Views/Usuarios/Acceso/Salir.cshtml");
            }
            catch
            {
                return View("~/Views/Usuarios/Acceso/Salir.cshtml");
            }
           
        }

        // GET: Acceso/Ingresar
        [HttpGet]
        public ActionResult Ingresar()
        {
            return View("~/Views/Usuarios/Acceso/Ingresar.cshtml");
        }

        // POST: Acceso/IniciarSesion
        //[HttpPost]
        public ActionResult IniciarSesion(Usuario usuario)
        {
            try
            {
                SesionPeticion peticion = new SesionPeticion
                {
                    Identificador = usuario.CorreoE,
                    Contrasena = usuario.Contrasenia,
                    IP = "127.0.0.1"
                };

                Servicio servicio = new Servicio();
                SesionRespuesta respuesta = servicio.IniciarSesion(peticion);
                
                if (respuesta.Usuario.Rol.RolId == 1)
                {
                    ViewBag.UserJson = respuesta.Usuario.Json;
                    return View("~/Views/Home/SuperAdministrador.cshtml");
                }

                if (usuario.CorreoE == "Sistemas")
                {
                    PreCargaPeticion preCargaPeticion = new PreCargaPeticion
                    {
                        Cabecero = new CabeceroPeticion
                        {
                            SesionId = respuesta.Usuario.SesionId
                        },

                        ColArchivo = new Collection<Archivo>()
                    };
                    preCargaPeticion.ColArchivo.Add(new Archivo
                    {
                        Ano = "2018",
                        Extension = "accdb",
                        Identificador = 1,                        
                        Nombre = "D:\\JOE\\DOCS\\CATALOGOS.accdb"
                    });
                   
                    servicio.PreCargarDatos(preCargaPeticion);
                    servicio.ProcesarCarga(preCargaPeticion);
                    return View("~/Views/Usuarios/Acceso/Ingresar.cshtml");
                }            


                if (respuesta.Cabecero.EsRespuestaExistosa() && respuesta.Usuario.SesionId > 0)
                {
                   return RedirectToAction("SeleccionarConsulta", "SubRegistro", new
                   {
                       userJson = respuesta.Usuario.Json
                   });
                    
                }
                else
                {
                   return View("~/Views/Usuarios/Acceso/Ingresar.cshtml");
                }
                
            }
            catch
            {
                return View("~/Views/Usuarios/Acceso/Ingresar.cshtml");
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