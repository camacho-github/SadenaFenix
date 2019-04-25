using Newtonsoft.Json;
using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Models.Usuarios;
using SadenaFenix.Services;
using SadenaFenix.Transport.Catalogos;
using SadenaFenix.Transport.Nacimientos.Archivos;
using SadenaFenix.Transport.Nacimientos.Reportes;
using System;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Web.Mvc;
using System.Web.Services;

namespace Sadena.Controllers.Nacimientos
{
    public class ReportesController : Controller
    {

        public ActionResult VerReportes(string userJson)
        {
            Usuario usuario = new Usuario { Json = userJson };
            return View("~/Views/Nacimientos/Reportes/Reportes.cshtml", usuario);
        }

        public ActionResult SeleccionarConsulta(string userJson)
        {
            /* Obtener json del usuario. */
            Usuario usuario = new Usuario { Json = userJson };
            ViewBag.UserJson = userJson;

            Servicio servicio = new Servicio();
            /* Obtener catalogos. */
            CatalogosCargasRespuesta catalogosCargasRespuesta = servicio.ObtenerCatalogosCargas(null);
            ViewBag.Anios = catalogosCargasRespuesta.ColAnios;
            ViewBag.Meses = catalogosCargasRespuesta.ColMeses;
            ViewBag.Municipios = catalogosCargasRespuesta.ColMunicipios;
            ViewBag.ModalTitulo = "Consulta de reportes";
            if (catalogosCargasRespuesta.ColAnios == null)
                return View("~/Views/Nacimientos/Archivos/Importar.cshtml", new ImportarArchivosViewModel());

            return View(catalogosCargasRespuesta);
        }


        [WebMethod]
        public ActionResult ReporteTotalesMunicipios(string AniosJson, string MesesJson, string MpiosJson)
        {
            Servicio servicio = new Servicio();
            dynamic anios = JsonConvert.DeserializeObject(AniosJson);
            dynamic meses = JsonConvert.DeserializeObject(MesesJson);
            dynamic mpios = JsonConvert.DeserializeObject(MpiosJson);

            ReporteTotalesSubregistroPeticion subregistroPeticion = new ReporteTotalesSubregistroPeticion
            {
                ColAnos = new Collection<string>()
            };
            foreach (string anio in anios)
            {
                subregistroPeticion.ColAnos.Add(anio);
            }

            subregistroPeticion.ColMeses = new Collection<string>();
            foreach (string mes in meses)
            {
                subregistroPeticion.ColMeses.Add(mes);
            }

            subregistroPeticion.ColMunicipios = new Collection<Municipio>();
            foreach (string mpio in mpios)
            {
                Municipio municipio = new Municipio
                {
                    MpioId = Convert.ToInt32(mpio)
                };

                subregistroPeticion.ColMunicipios.Add(municipio);
            }

            TotalesMunicipiosRespuesta respuesta = new TotalesMunicipiosRespuesta();
            respuesta = servicio.ConsultarReporteTotalesMunicipio(subregistroPeticion);

            return Json(respuesta, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ReportesEdad(string AniosJson, string MesesJson, string MpiosJson)
        {
            Servicio servicio = new Servicio();
            dynamic anios = JsonConvert.DeserializeObject(AniosJson);
            dynamic meses = JsonConvert.DeserializeObject(MesesJson);
            dynamic mpios = JsonConvert.DeserializeObject(MpiosJson);

            ReporteEdadSubregistroPeticion reporteEdadSubregistroPeticion = new ReporteEdadSubregistroPeticion
            {
                ColAnos = new Collection<string>()
            };
            foreach (string anio in anios)
            {
                reporteEdadSubregistroPeticion.ColAnos.Add(anio);               
            }

            reporteEdadSubregistroPeticion.ColMeses = new Collection<string>();
            foreach (string mes in meses)
            {
                reporteEdadSubregistroPeticion.ColMeses.Add(mes);               
            }

            reporteEdadSubregistroPeticion.ColMunicipios = new Collection<Municipio>();           
            foreach (string mpio in mpios)
            {
                Municipio municipio = new Municipio
                {
                    MpioId = Convert.ToInt32(mpio)
                };

                reporteEdadSubregistroPeticion.ColMunicipios.Add(municipio);                
            }

            ReporteSubregistroRespuesta respuesta = new ReporteSubregistroRespuesta();
            respuesta = servicio.ConsultarReporteEdadSubregistro(reporteEdadSubregistroPeticion);
            
            dynamic model = new ExpandoObject();
            model.ReporteSubRegistros = respuesta.DTs[0];
            model.ReporteOportunos = respuesta.DTs[1];
            model.ReporteExtemporaneos = respuesta.DTs[2];

            //if (Request.IsAjaxRequest())
            return PartialView(model);

        }

        public ActionResult ReportesEdoCivil(string AniosJson, string MesesJson, string MpiosJson)
        {
            Servicio servicio = new Servicio();
            dynamic anios = JsonConvert.DeserializeObject(AniosJson);
            dynamic meses = JsonConvert.DeserializeObject(MesesJson);
            dynamic mpios = JsonConvert.DeserializeObject(MpiosJson);

            ReporteEdoCivilSubregistroPeticion reportePeticion = new ReporteEdoCivilSubregistroPeticion
            {
                ColAnos = new Collection<string>()
            };
            foreach (string anio in anios)
            {
                reportePeticion.ColAnos.Add(anio);
            }

            reportePeticion.ColMeses = new Collection<string>();
            foreach (string mes in meses)
            {
                reportePeticion.ColMeses.Add(mes);
            }

            reportePeticion.ColMunicipios = new Collection<Municipio>();
            foreach (string mpio in mpios)
            {
                Municipio municipio = new Municipio
                {
                    MpioId = Convert.ToInt32(mpio)
                };

                reportePeticion.ColMunicipios.Add(municipio);
            }

            ReporteSubregistroRespuesta respuesta = new ReporteSubregistroRespuesta();
            respuesta = servicio.ConsultarReporteEdoCivilSubregistro(reportePeticion);

            dynamic model = new ExpandoObject();
            model.ReporteSubRegistros = respuesta.DTs[0];
            model.ReporteOportunos = respuesta.DTs[1];
            model.ReporteExtemporaneos = respuesta.DTs[2];

            //if (Request.IsAjaxRequest())
            return PartialView(model);

        }

        public ActionResult ReportesNumNac(string AniosJson, string MesesJson, string MpiosJson)
        {
            Servicio servicio = new Servicio();
            dynamic anios = JsonConvert.DeserializeObject(AniosJson);
            dynamic meses = JsonConvert.DeserializeObject(MesesJson);
            dynamic mpios = JsonConvert.DeserializeObject(MpiosJson);

            ReporteNumNacSubregistroPeticion reportePeticion = new ReporteNumNacSubregistroPeticion
            {
                ColAnos = new Collection<string>()
            };
            foreach (string anio in anios)
            {
                reportePeticion.ColAnos.Add(anio);
            }

            reportePeticion.ColMeses = new Collection<string>();
            foreach (string mes in meses)
            {
                reportePeticion.ColMeses.Add(mes);
            }

            reportePeticion.ColMunicipios = new Collection<Municipio>();
            foreach (string mpio in mpios)
            {
                Municipio municipio = new Municipio
                {
                    MpioId = Convert.ToInt32(mpio)
                };

                reportePeticion.ColMunicipios.Add(municipio);
            }

            ReporteSubregistroRespuesta respuesta = new ReporteSubregistroRespuesta();
            respuesta = servicio.ConsultarReporteNumNacSubregistro(reportePeticion);

            dynamic model = new ExpandoObject();
            model.ReporteSubRegistros = respuesta.DTs[0];
            model.ReporteOportunos = respuesta.DTs[1];
            model.ReporteExtemporaneos = respuesta.DTs[2];

            //if (Request.IsAjaxRequest())
            return PartialView(model);

        }

        public ActionResult ReportesEscolaridad(string AniosJson, string MesesJson, string MpiosJson)
        {
            Servicio servicio = new Servicio();
            dynamic anios = JsonConvert.DeserializeObject(AniosJson);
            dynamic meses = JsonConvert.DeserializeObject(MesesJson);
            dynamic mpios = JsonConvert.DeserializeObject(MpiosJson);

            ReporteEscolaridadSubregistroPeticion reportePeticion = new ReporteEscolaridadSubregistroPeticion
            {
                ColAnos = new Collection<string>()
            };
            foreach (string anio in anios)
            {
                reportePeticion.ColAnos.Add(anio);
            }

            reportePeticion.ColMeses = new Collection<string>();
            foreach (string mes in meses)
            {
                reportePeticion.ColMeses.Add(mes);
            }

            reportePeticion.ColMunicipios = new Collection<Municipio>();
            foreach (string mpio in mpios)
            {
                Municipio municipio = new Municipio
                {
                    MpioId = Convert.ToInt32(mpio)
                };

                reportePeticion.ColMunicipios.Add(municipio);
            }

            ReporteSubregistroRespuesta respuesta = new ReporteSubregistroRespuesta();
            respuesta = servicio.ConsultarReporteEscolaridadSubregistro(reportePeticion);

            dynamic model = new ExpandoObject();
            model.ReporteSubRegistros = respuesta.DTs[0];
            model.ReporteOportunos = respuesta.DTs[1];
            model.ReporteExtemporaneos = respuesta.DTs[2];

            //if (Request.IsAjaxRequest())
            return PartialView(model);

        }

        public ActionResult ReportesSexo(string AniosJson, string MesesJson, string MpiosJson)
        {
            Servicio servicio = new Servicio();
            dynamic anios = JsonConvert.DeserializeObject(AniosJson);
            dynamic meses = JsonConvert.DeserializeObject(MesesJson);
            dynamic mpios = JsonConvert.DeserializeObject(MpiosJson);

            ReporteSexoSubregistroPeticion reportePeticion = new ReporteSexoSubregistroPeticion
            {
                ColAnos = new Collection<string>()
            };
            foreach (string anio in anios)
            {
                reportePeticion.ColAnos.Add(anio);
            }

            reportePeticion.ColMeses = new Collection<string>();
            foreach (string mes in meses)
            {
                reportePeticion.ColMeses.Add(mes);
            }

            reportePeticion.ColMunicipios = new Collection<Municipio>();
            foreach (string mpio in mpios)
            {
                Municipio municipio = new Municipio
                {
                    MpioId = Convert.ToInt32(mpio)
                };

                reportePeticion.ColMunicipios.Add(municipio);
            }

            ReporteSubregistroRespuesta respuesta = new ReporteSubregistroRespuesta();
            respuesta = servicio.ConsultarReporteSexoSubregistro(reportePeticion);

            dynamic model = new ExpandoObject();
            model.ReporteSubRegistros = respuesta.DTs[0];
            model.ReporteOportunos = respuesta.DTs[1];
            model.ReporteExtemporaneos = respuesta.DTs[2];

            //if (Request.IsAjaxRequest())
            return PartialView(model);

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