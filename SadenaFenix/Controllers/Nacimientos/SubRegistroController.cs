﻿using Newtonsoft.Json;
using SadenaFenix.Models.Catalogos.Geografia;
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

    public class SubRegistroController : Controller
    {

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
            ViewBag.ModalTitulo = "Consulta del subregistro";

            if(catalogosCargasRespuesta.ColAnios==null)
                return View("~/Views/Nacimientos/Archivos/Importar.cshtml", new ImportarArchivosViewModel());

            return View(catalogosCargasRespuesta);
        }
       

        public ActionResult SubRegistroInformacion(string AniosJson,string MesesJson, string MpiosJson, string MesesDesc, string AniosDesc,string MpiosDesc)
        {
            Servicio servicio = new Servicio();
            dynamic anios = JsonConvert.DeserializeObject(AniosJson);
            dynamic meses = JsonConvert.DeserializeObject(MesesJson);
            dynamic mpios = JsonConvert.DeserializeObject(MpiosJson);

            SubregistroPeticion peticionSubRegistro = new SubregistroPeticion();
            ReporteTotalesSubregistroPeticion peticionReporte = new ReporteTotalesSubregistroPeticion();

            peticionSubRegistro.ColAnos = new Collection<string>();
            peticionReporte.ColAnos = new Collection<string>();
            foreach (string anio in anios)
            {
                peticionSubRegistro.ColAnos.Add(anio);
                peticionReporte.ColAnos.Add(anio);
            }

            peticionSubRegistro.ColMeses = new Collection<string>();
            peticionReporte.ColMeses = new Collection<string>();
            foreach (string mes in meses)
            {
                peticionSubRegistro.ColMeses.Add(mes);
                peticionReporte.ColMeses.Add(mes);
            }

            peticionSubRegistro.ColMunicipios = new Collection<Municipio>();
            peticionReporte.ColMunicipios = new Collection<Municipio>();
            foreach (string mpio in mpios)
            {
                Municipio municipio = new Municipio
                {
                    MpioId = Convert.ToInt32(mpio)
                };

                peticionSubRegistro.ColMunicipios.Add(municipio);
                peticionReporte.ColMunicipios.Add(municipio);
            }

            SubregistroNacimientosRespuesta SubregistroRespuesta = servicio.ConsultaSubregistroNacimientos(peticionSubRegistro);
            
            ReporteSubregistroRespuesta reporteRespuesta = servicio.ConsultarReporteTotalesSubregistro(peticionReporte);
            
            dynamic model = new ExpandoObject();
            model.TotalSubregistro = SubregistroRespuesta.TotalSubregistro;
            model.PorcentajeSubregistro = SubregistroRespuesta.PorcentajeSubregistro;

            model.TotalRegistroOportuno = SubregistroRespuesta.TotalRegistroOportuno;
            model.PorcentajeRegistroOportuno = SubregistroRespuesta.PorcentajeRegistroOportuno;

            model.TotalRegistroExtemporaneo = SubregistroRespuesta.TotalRegistroExtemporaneo;
            model.PorcentajeRegistroExtemporaneo = SubregistroRespuesta.PorcentajeRegistroExtemporaneo;

            model.TotalRegistroDuplicado = SubregistroRespuesta.TotalRegistroDuplicado;
            model.PorcentajeRegistroDuplicado = SubregistroRespuesta.PorcentajeRegistroDuplicado;

            model.TotalGeneral = SubregistroRespuesta.Total;
            model.PorcentajeGeneral = SubregistroRespuesta.TotalPorcentaje;

            model.ColDataTables = SubregistroRespuesta.ColDataTables;

            model.ColCabeceros = reporteRespuesta.ColCabeceros;
            model.ColFilas = reporteRespuesta.ColFilas;

            model.FechaReporte = DateTime.Now.ToString("dd/MM/yyyy h:mm tt", CultureInfo.InvariantCulture);
            model.MesesReporte = string.IsNullOrEmpty(MesesDesc) ? "Todos": MesesDesc;
            model.AniosReporte = string.IsNullOrEmpty(AniosDesc) ? "Todos" : AniosDesc;
            model.MpiosReporte = string.IsNullOrEmpty(MpiosDesc) ? "Todos" : MpiosDesc;

            //if (Request.IsAjaxRequest())
            return PartialView(model);
            
        }


    }
}