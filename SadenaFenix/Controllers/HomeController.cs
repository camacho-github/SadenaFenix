﻿using Newtonsoft.Json;
using SadenaFenix.Models.Usuarios;
using SadenaFenix.Services;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Web.Mvc;

namespace SadenaFenix.Controllers
{
    public class HomeController : Controller
    {
        // Get: Index
        public ActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }

        public ActionResult About(string userJson)
        {
            if (userJson == null)
            {
                return View("~/Views/Home/About.cshtml", new Usuario());
            }
            else
            {
                Usuario usuario = new Usuario { Json = userJson };

                return View("~/Views/Home/About.cshtml", usuario);
            }
            
        }
       
        public ActionResult Contact(string userJson)
        {
            if (userJson == null)
            {
                return View("~/Views/Home/Contact.cshtml", new Usuario());
            }else
            {
                Usuario usuario = new Usuario { Json = userJson };
                return View("~/Views/Home/Contact.cshtml", usuario);
            }
        }



        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult Contact2()
        {
            SesionRespuesta resultadoIniciarSesion = IniciarSesion();
            ViewBag.Message = "Resultados de servicio de iniciar Sesión " + JsonConvert.SerializeObject(resultadoIniciarSesion);

            SesionPeticion sesionPeticion = new SesionPeticion
            {
                Cabecero = new CabeceroPeticion
                {
                    SesionId = resultadoIniciarSesion.Usuario.SesionId
                }
            };

            SesionRespuesta resultadoFinalizarSesion = FinalizarSesion(sesionPeticion);
            ViewBag.Message = ViewBag.Message + "\n\n\n Resultados de servicio de Finalizar Sesión " + JsonConvert.SerializeObject(resultadoFinalizarSesion);


            return View();
        }

        private SesionRespuesta IniciarSesion()
        {

            SesionPeticion sesionPeticion = new SesionPeticion
            {
                Identificador = "Administrador",
                Contrasena = "Administrador123",
                IP = "127.0.0.1"
            };

            Servicio servicio = new Servicio();
            SesionRespuesta sesionRespuesta = servicio.IniciarSesion(sesionPeticion);

            return sesionRespuesta;
            
        }

        private SesionRespuesta FinalizarSesion(SesionPeticion sesionPeticion)
        {

            Servicio servicio = new Servicio();
            SesionRespuesta sesionRespuesta = servicio.FinalizarSesion(sesionPeticion);


            return sesionRespuesta;
        }

        /*
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        */
    }
}