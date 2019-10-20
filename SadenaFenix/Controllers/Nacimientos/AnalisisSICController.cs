using Newtonsoft.Json;
using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Models.Catalogos.Tiempo;
using SadenaFenix.Models.Usuarios;
using SadenaFenix.Services;
using SadenaFenix.Transport.Catalogos;
using SadenaFenix.Transport.Nacimientos.Archivos;
using SadenaFenix.Transport.Nacimientos.Reportes;
using System;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Globalization;
using System.Web.Mvc;


namespace SadenaFenix.Controllers.Nacimientos
{

    public class AnalisisSICController: Controller
    {

        public ActionResult SeleccionarConsultaSIC(string userJson)
        {
            /* Obtener json del usuario. */
            Usuario usuario = new Usuario { Json = userJson };
            ViewBag.UserJson = userJson;

            Servicio servicio = new Servicio();
            /* Obtener catalogos. */
            CatalogosCargasSICRespuesta catalogosCargasRespuesta = servicio.ObtenerCatalogosSICCargas(null);
            ViewBag.AniosNac = catalogosCargasRespuesta.ColAniosNac;
            ViewBag.AniosRegistro = catalogosCargasRespuesta.ColAniosRegistro;
            ViewBag.Meses = catalogosCargasRespuesta.ColMeses;
            ViewBag.Municipios = catalogosCargasRespuesta.ColMunicipios;
            ViewBag.ModalTitulo = "Consultar análisis de información SIC";

            if(catalogosCargasRespuesta.ColAniosRegistro == null)
                return View("~/Views/Nacimientos/Archivos/Importar.cshtml", new ImportarArchivosViewModel());

            return View(catalogosCargasRespuesta);
        }
       

        public ActionResult CoberturaSIC(string AniosRegistroJson, string AniosNacimientoJson, string MesesJson, string MpiosJson)
        {
            Servicio servicio = new Servicio();
            dynamic aniosNac = JsonConvert.DeserializeObject(AniosNacimientoJson);
            dynamic aniosReg = JsonConvert.DeserializeObject(AniosRegistroJson);
            dynamic meses = JsonConvert.DeserializeObject(MesesJson);
            dynamic mpios = JsonConvert.DeserializeObject(MpiosJson);

            AnalisisSICPeticion peticion = new AnalisisSICPeticion
            {
                ColAnosNac = new Collection<string>(),
                ColAnosReg = new Collection<string>(),
                ColMeses = new Collection<string>(),
                ColMunicipios = new Collection<Municipio>()
        };
            foreach (string anio in aniosNac)
            {
                peticion.ColAnosNac.Add(anio);                
            }
            foreach (string anio in aniosReg)
            {
                peticion.ColAnosReg.Add(anio);
            }
            foreach (string mes in meses)
            {
                peticion.ColMeses.Add(mes);
            }                      
            foreach (string mpio in mpios)
            {
                Municipio municipio = new Municipio
                {
                    MpioId = Convert.ToInt32(mpio)
                };

                peticion.ColMunicipios.Add(municipio);                
            }

            AnalisisSICRespuesta AnalisisSICRespuesta = servicio.ConsultarAnalisisInformacionSIC(peticion);

            dynamic model = new ExpandoObject();
            model.OportunoRelacionPorFolio = AnalisisSICRespuesta.DTs[0];
            model.OportunoRelacionPorFecha = AnalisisSICRespuesta.DTs[1];
            
            //if (Request.IsAjaxRequest())
            return PartialView(model);
            
        }

        public ActionResult CoberturaSIC2(string AniosRegistroJson, string AniosNacimientoJson, string MesesJson, string MpiosJson)
        {
            Servicio servicio = new Servicio();
            dynamic aniosNac = JsonConvert.DeserializeObject(AniosNacimientoJson);
            dynamic aniosReg = JsonConvert.DeserializeObject(AniosRegistroJson);
            dynamic meses = JsonConvert.DeserializeObject(MesesJson);
            dynamic mpios = JsonConvert.DeserializeObject(MpiosJson);

            AnalisisSICPeticion peticion = new AnalisisSICPeticion
            {
                ColAnosNac = new Collection<string>(),
                ColAnosReg = new Collection<string>(),
                ColMeses = new Collection<string>(),
                ColMunicipios = new Collection<Municipio>()
            };
            foreach (string anio in aniosNac)
            {
                peticion.ColAnosNac.Add(anio);
            }
            foreach (string anio in aniosReg)
            {
                peticion.ColAnosReg.Add(anio);
            }
            foreach (string mes in meses)
            {
                peticion.ColMeses.Add(mes);
            }
            foreach (string mpio in mpios)
            {
                Municipio municipio = new Municipio
                {
                    MpioId = Convert.ToInt32(mpio)
                };

                peticion.ColMunicipios.Add(municipio);
            }

            AnalisisSICRespuesta AnalisisSICRespuesta = servicio.ConsultarAnalisisInformacionSIC(peticion);

            dynamic model = new ExpandoObject();
            model.ExtemporaneoRelacionPorFolio = AnalisisSICRespuesta.DTs[2];
            model.ExtemporaneoRelacionPorFecha = AnalisisSICRespuesta.DTs[3];

            //if (Request.IsAjaxRequest())
            return PartialView(model);

        }

        public ActionResult CoberturaSIC3(string AniosRegistroJson, string AniosNacimientoJson, string MesesJson, string MpiosJson)
        {
            Servicio servicio = new Servicio();
            dynamic aniosNac = JsonConvert.DeserializeObject(AniosNacimientoJson);
            dynamic aniosReg = JsonConvert.DeserializeObject(AniosRegistroJson);
            dynamic meses = JsonConvert.DeserializeObject(MesesJson);
            dynamic mpios = JsonConvert.DeserializeObject(MpiosJson);

            AnalisisSICPeticion peticion = new AnalisisSICPeticion
            {
                ColAnosNac = new Collection<string>(),
                ColAnosReg = new Collection<string>(),
                ColMeses = new Collection<string>(),
                ColMunicipios = new Collection<Municipio>()
            };
            foreach (string anio in aniosNac)
            {
                peticion.ColAnosNac.Add(anio);
            }
            foreach (string anio in aniosReg)
            {
                peticion.ColAnosReg.Add(anio);
            }
            foreach (string mes in meses)
            {
                peticion.ColMeses.Add(mes);
            }
            foreach (string mpio in mpios)
            {
                Municipio municipio = new Municipio
                {
                    MpioId = Convert.ToInt32(mpio)
                };

                peticion.ColMunicipios.Add(municipio);
            }

            AnalisisSICRespuesta AnalisisSICRespuesta = servicio.ConsultarAnalisisInformacionSICConTotales(peticion);

            dynamic model = new ExpandoObject();
            model.OportunoSinRelacion = AnalisisSICRespuesta.DTs[4];
            model.ExtemporaneoSinRelacion = AnalisisSICRespuesta.DTs[5];
            model.Totales = JsonConvert.SerializeObject(AnalisisSICRespuesta.TotalesCoberturaRegistral);

            //if (Request.IsAjaxRequest())
            return PartialView(model);

        }

        public ActionResult TotalSINAC(string AniosNacimientoJson, string MesesJson, string MpiosJson , string MesesDesc, string AniosDesc, string AniosRegDesc, string MpiosDesc)
        {
            Servicio servicio = new Servicio();
            dynamic aniosNac = JsonConvert.DeserializeObject(AniosNacimientoJson);
            dynamic meses = JsonConvert.DeserializeObject(MesesJson);
            dynamic mpios = JsonConvert.DeserializeObject(MpiosJson);

            AnalisisSICPeticion peticion = new AnalisisSICPeticion
            {
                ColAnosNac = new Collection<string>(),
                ColAnosReg = new Collection<string>(),
                ColMeses = new Collection<string>(),
                ColMunicipios = new Collection<Municipio>()
            };
            foreach (string anio in aniosNac)
            {
                peticion.ColAnosNac.Add(anio);
            }
            foreach (string mes in meses)
            {
                peticion.ColMeses.Add(mes);
            }
            foreach (string mpio in mpios)
            {
                Municipio municipio = new Municipio
                {
                    MpioId = Convert.ToInt32(mpio)
                };

                peticion.ColMunicipios.Add(municipio);
            }

            AnalisisSICRespuesta AnalisisSICRespuesta = servicio.ConsultarTotalSINAC(peticion);

            dynamic model = new ExpandoObject();
            model.TotalSINAC = AnalisisSICRespuesta.TotalSINAC;
            model.FechaReporte = DateTime.Now.ToString("dd/MM/yyyy h:mm tt", CultureInfo.InvariantCulture);
            model.MesesReporte = string.IsNullOrEmpty(MesesDesc) ? "Todos" : MesesDesc;
            model.AniosReporte = string.IsNullOrEmpty(AniosDesc) ? "Todos" : AniosDesc;
            model.AniosRegReporte = string.IsNullOrEmpty(AniosRegDesc) ? "Todos" : AniosRegDesc;
            model.MpiosReporte = string.IsNullOrEmpty(MpiosDesc) ? "Todos" : MpiosDesc;

            //if (Request.IsAjaxRequest())
            return PartialView(model);

        }

        public ActionResult InconsistenciasSIC(string AniosRegistroJson, string AniosNacimientoJson, string MesesJson, string MpiosJson)
        {
            Servicio servicio = new Servicio();
            dynamic aniosNac = JsonConvert.DeserializeObject(AniosNacimientoJson);
            dynamic aniosReg = JsonConvert.DeserializeObject(AniosRegistroJson);
            dynamic meses = JsonConvert.DeserializeObject(MesesJson);
            dynamic mpios = JsonConvert.DeserializeObject(MpiosJson);

            AnalisisSICPeticion peticion = new AnalisisSICPeticion
            {
                ColAnosNac = new Collection<string>(),
                ColAnosReg = new Collection<string>(),
                ColMeses = new Collection<string>(),
                ColMunicipios = new Collection<Municipio>()
            };
            foreach (string anio in aniosNac)
            {
                peticion.ColAnosNac.Add(anio);
            }
            foreach (string anio in aniosReg)
            {
                peticion.ColAnosReg.Add(anio);
            }
            foreach (string mes in meses)
            {
                peticion.ColMeses.Add(mes);
            }
            foreach (string mpio in mpios)
            {
                Municipio municipio = new Municipio
                {
                    MpioId = Convert.ToInt32(mpio)
                };

                peticion.ColMunicipios.Add(municipio);
            }

            AnalisisSICRespuesta AnalisisSICRespuesta = servicio.ConsultarInconsistenciasSIC(peticion);

            dynamic model = new ExpandoObject();
            model.CaracteresEspeciales = AnalisisSICRespuesta.DTs[0];
            model.DuplicadosSIC = AnalisisSICRespuesta.DTs[1];

            //if (Request.IsAjaxRequest())
            return PartialView(model);

        }


        public ActionResult OtrosFoliosSIC(string AniosRegistroJson, string AniosNacimientoJson, string MesesJson, string MpiosJson)
        {
            Servicio servicio = new Servicio();
            dynamic aniosNac = JsonConvert.DeserializeObject(AniosNacimientoJson);
            dynamic aniosReg = JsonConvert.DeserializeObject(AniosRegistroJson);
            dynamic meses = JsonConvert.DeserializeObject(MesesJson);
            dynamic mpios = JsonConvert.DeserializeObject(MpiosJson);

            AnalisisSICPeticion peticion = new AnalisisSICPeticion
            {
                ColAnosNac = new Collection<string>(),
                ColAnosReg = new Collection<string>(),
                ColMeses = new Collection<string>(),
                ColMunicipios = new Collection<Municipio>()
            };
            foreach (string anio in aniosNac)
            {
                peticion.ColAnosNac.Add(anio);
            }
            foreach (string anio in aniosReg)
            {
                peticion.ColAnosReg.Add(anio);
            }
            foreach (string mes in meses)
            {
                peticion.ColMeses.Add(mes);
            }
            foreach (string mpio in mpios)
            {
                Municipio municipio = new Municipio
                {
                    MpioId = Convert.ToInt32(mpio)
                };

                peticion.ColMunicipios.Add(municipio);
            }

            AnalisisSICRespuesta AnalisisSICRespuesta = servicio.ConsultarOtrosFolios(peticion);

            dynamic model = new ExpandoObject();
            model.OtrosEstados = AnalisisSICRespuesta.DTs[0];
            model.OtrosAnos = AnalisisSICRespuesta.DTs[1];

            //if (Request.IsAjaxRequest())
            return PartialView(model);

        }


    }
}