using SadenaFenix.Models.Usuarios;
using SadenaFenix.Services;
using SadenaFenix.Transport.Usuarios.Acceso;
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
                SesionPeticion peticion = new SesionPeticion();
                peticion.Identificador = usuario.CorreoE;
                peticion.Contrasena = usuario.Contrasenia;
                peticion.IP = "127.0.0.1";

                Servicio servicio = new Servicio();
                SesionRespuesta respuesta = servicio.IniciarSesion(peticion);

                if(respuesta.Cabecero.EsRespuestaExistosa() && respuesta.Usuario.SesionId > 0)
                {
                   return RedirectToAction("LoginConsultar", "Consultas", respuesta.Usuario);
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