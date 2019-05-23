using Newtonsoft.Json;
using SadenaFenix.Models.Nacimientos.Archivos;
using SadenaFenix.Models.Usuarios;
using SadenaFenix.Services;
using SadenaFenix.Transport.Nacimientos.Archivos;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using System.Web.Services;

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

        public ActionResult Salir(string userJson)
        {            
            try
            {
                Usuario usuario = JsonConvert.DeserializeObject<Usuario>(userJson);
                usuario.Json = userJson;
                ViewBag.UserJson = userJson;

                SesionPeticion peticion = new SesionPeticion();
                peticion.Cabecero = new CabeceroPeticion();
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
                    IP = usuario.IP
                };

                if(peticion.IP == null)
                {
                    peticion.IP = "127.0.0.1";
                }

                Servicio servicio = new Servicio();
                SesionRespuesta respuesta = servicio.IniciarSesion(peticion);
                if(!respuesta.Cabecero.EsRespuestaExistosa())
                {
                    ViewBag.ErrorAcceso = respuesta.Cabecero.MensajeRespuesta;
                    return View("~/Views/Usuarios/Acceso/Ingresar.cshtml");
                }
                
                if (respuesta.Usuario.Rol.RolId == 1)
                {
                    ViewBag.UserJson = respuesta.Usuario.Json;
                    return View("~/Views/Home/SuperAdministrador.cshtml");
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

        public ActionResult CargaCatalogos(string userJson)
        {
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(userJson);
            usuario.Json = userJson;
            ViewBag.UserJson = userJson;

            PreCargaPeticion preCargaPeticion = new PreCargaPeticion
            {
                Cabecero = new CabeceroPeticion
                {
                    SesionId = usuario.SesionId
                },

                ColArchivo = new Collection<Archivo>()
            };
            preCargaPeticion.ColArchivo.Add(new Archivo
            {
                Ano = "2019",
                Extension = "accdb",
                Identificador = 1,
                Nombre = "C:\\inetpub\\wwwroot\\Sadena\\SadenaFenix\\CATALOGOS.accdb"
            });
            ViewBag.ResultadoCarga = "-1";
            Servicio servicio = new Servicio();
            CabeceroRespuesta respuesta = servicio.PreCargarDatos(preCargaPeticion);
            if (respuesta.CodigoRespuesta == 0)
            {
                respuesta = servicio.ProcesarCarga(preCargaPeticion);
                if (respuesta.CodigoRespuesta == 0)
                {
                    ViewBag.ResultadoCarga = 0;
                }
            }
            else if (respuesta.CodigoRespuesta == 2)
            {
                ViewBag.ResultadoCarga = 2;
            }
            return View("~/Views/Home/SuperAdministrador.cshtml");
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