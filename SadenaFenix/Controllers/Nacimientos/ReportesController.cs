using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Models.Usuarios;
using SadenaFenix.Services;
using SadenaFenix.Transport.Nacimientos.Reportes;
using System.Collections.ObjectModel;
using System.Web.Mvc;
using System.Web.Services;

namespace Sadena.Controllers.Nacimientos
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VerReportes(string userJson)
        {
            Usuario usuario = new Usuario { Json = userJson };
            return View("~/Views/Nacimientos/Reportes/Reportes.cshtml",usuario);
        }

        // GET: Reportes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reportes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reportes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Reportes/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reportes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Reportes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reportes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [WebMethod]
        public ActionResult ConsultarReporteSexoSubregistro()
        {
            Servicio servicio = new Servicio();
            ReporteSexoSubregistroPeticion peticion = new ReporteSexoSubregistroPeticion();
            Collection<string> ColAnos = new Collection<string>();
            ColAnos.Add("2017");
            ColAnos.Add("2018");

            Collection<string> ColMeses = new Collection<string>();
            ColMeses.Add("1");
            ColMeses.Add("2");
            ColMeses.Add("3");

            Collection<Municipio> ColMunicipio = new Collection<Municipio>();

            peticion.ColAnos = ColAnos;
            peticion.ColMeses = ColMeses;
            peticion.ColMunicipios = ColMunicipio;

            ReporteSubregistroRespuesta respuesta = new ReporteSubregistroRespuesta();
            respuesta = servicio.ConsultarReporteSexoSubregistro(peticion);

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [WebMethod]
        public ActionResult ConsultarReporteEdoCivilSubregistro()
        {
            Servicio servicio = new Servicio();
            ReporteEdoCivilSubregistroPeticion peticion = new ReporteEdoCivilSubregistroPeticion();
            Collection<string> ColAnos = new Collection<string>();
            ColAnos.Add("2017");
            ColAnos.Add("2018");

            Collection<string> ColMeses = new Collection<string>();
            ColMeses.Add("1");
            ColMeses.Add("2");
            ColMeses.Add("3");

            Collection<Municipio> ColMunicipio = new Collection<Municipio>();

            peticion.ColAnos = ColAnos;
            peticion.ColMeses = ColMeses;
            peticion.ColMunicipios = ColMunicipio;

            ReporteSubregistroRespuesta respuesta = new ReporteSubregistroRespuesta();
            respuesta = servicio.ConsultarReporteEdoCivilSubregistro(peticion);

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [WebMethod]
        public ActionResult ConsultarReporteEdadSubregistro()
        {
            Servicio servicio = new Servicio();
            ReporteEdadSubregistroPeticion peticion = new ReporteEdadSubregistroPeticion();
            Collection<string> ColAnos = new Collection<string>();
            ColAnos.Add("2017");
            ColAnos.Add("2018");

            Collection<string> ColMeses = new Collection<string>();
            ColMeses.Add("1");
            ColMeses.Add("2");
            ColMeses.Add("3");

            Collection<Municipio> ColMunicipio = new Collection<Municipio>();

            peticion.ColAnos = ColAnos;
            peticion.ColMeses = ColMeses;
            peticion.ColMunicipios = ColMunicipio;

            ReporteSubregistroRespuesta respuesta = new ReporteSubregistroRespuesta();
            respuesta = servicio.ConsultarReporteEdadSubregistro(peticion);

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [WebMethod]
        public ActionResult ConsultarReporteEscolaridadSubregistro()
        {
            Servicio servicio = new Servicio();
            ReporteEscolaridadSubregistroPeticion peticion = new ReporteEscolaridadSubregistroPeticion();
            Collection<string> ColAnos = new Collection<string>();
            ColAnos.Add("2017");
            ColAnos.Add("2018");

            Collection<string> ColMeses = new Collection<string>();
            ColMeses.Add("1");
            ColMeses.Add("2");
            ColMeses.Add("3");

            Collection<Municipio> ColMunicipio = new Collection<Municipio>();

            peticion.ColAnos = ColAnos;
            peticion.ColMeses = ColMeses;
            peticion.ColMunicipios = ColMunicipio;

            ReporteSubregistroRespuesta respuesta = new ReporteSubregistroRespuesta();
            respuesta = servicio.ConsultarReporteEscolaridadSubregistro(peticion);

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }

        [WebMethod]
        public ActionResult ConsultarReporteNumNacSubregistro()
        {
            Servicio servicio = new Servicio();
            ReporteNumNacSubregistroPeticion peticion = new ReporteNumNacSubregistroPeticion();
            Collection<string> ColAnos = new Collection<string>();
            ColAnos.Add("2017");
            ColAnos.Add("2018");

            Collection<string> ColMeses = new Collection<string>();
            ColMeses.Add("1");
            ColMeses.Add("2");
            ColMeses.Add("3");

            Collection<Municipio> ColMunicipio = new Collection<Municipio>();

            peticion.ColAnos = ColAnos;
            peticion.ColMeses = ColMeses;
            peticion.ColMunicipios = ColMunicipio;

            ReporteSubregistroRespuesta respuesta = new ReporteSubregistroRespuesta();
            respuesta = servicio.ConsultarReporteNumNacSubregistro(peticion);

            return Json(respuesta, JsonRequestBehavior.AllowGet);
        }
    }
}