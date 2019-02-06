using SadenaFenix.Facade.Nacimientos.Consultas;
using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Models.Usuarios;
using SadenaFenix.Services;
using SadenaFenix.Transport.Nacimientos.Consultas;
using SadenaFenix.Transport.Nacimientos.Consultas.Comboxes;
using SadenaFenix.Transport.Nacimientos.Reportes;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using System.Web.Services;

namespace SadenaFenix.Controllers.Nacimientos
{

    public class ConsultasController : Controller
    {

        // GET: Consultas/Consultar
        
        public ActionResult Consultar(string userJson)
        {            
            ConsultaFacade consultaFacade = new ConsultaFacade();
            Usuario usuario = new Usuario { Json = userJson };
            ConsultasViewModel model = consultaFacade.ObtenerCalatogosParaConsulta();
            model.Usuario = usuario;
            return View("~/Views/Nacimientos/Consultas/Consultar.cshtml", model);
        }

        [HttpGet]
        public ActionResult LoginConsultar(Usuario usuario)
        {
            ConsultaFacade consultaFacade = new ConsultaFacade();
            Usuario modelUsuario = new Usuario { Json = usuario.Json };
            ConsultasViewModel model = consultaFacade.ObtenerCalatogosParaConsulta();
            model.Usuario = usuario;
            return View("~/Views/Nacimientos/Consultas/Consultar.cshtml", model);
        }

        // GET: Consultas/ConsultarTotales
        [WebMethod]
        public ActionResult ConsultarTotales()
        {
            TotalesSubregistroNacimientosRespuesta totales = new TotalesSubregistroNacimientosRespuesta();
            Servicio servicio = new Servicio();
            SubregistroPeticion peticion = new SubregistroPeticion();
            Collection<string> ColAnos = new Collection<string>();
            ColAnos.Add("2017");
            ColAnos.Add("2018");

            Collection<string> ColMeses = new Collection<string>();
            ColAnos.Add("1");
            ColAnos.Add("2");
            ColAnos.Add("3");

            Collection<Municipio> ColMunicipio = new Collection<Municipio>();
            ColMunicipio.Add(new Municipio {MpioId = 1 });
            ColMunicipio.Add(new Municipio {MpioId = 2 });
            ColMunicipio.Add(new Municipio {MpioId = 5 });
            ColMunicipio.Add(new Municipio {MpioId = 7 });

            peticion.ColAnos = ColAnos;
            peticion.ColMeses = ColMeses;
            peticion.ColMunicipios = ColMunicipio;

            totales = servicio.ConsultaTotalesSubregistroNacimientos(peticion);
            return Json(totales, JsonRequestBehavior.AllowGet);
        }

        // POST: Consultas/Index
        [HttpPost]
        public ActionResult Index(MesViewModelIEnumerable model)
        {
            if (ModelState.IsValid)
            {
                var msg = model.MesesSeleccionados;
            }

            // If we got this far, something failed; redisplay form.
            return View(model);
        }


    }
}