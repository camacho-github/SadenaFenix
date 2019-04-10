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
using System.Web.Mvc;


namespace SadenaFenix.Controllers.Nacimientos
{

    public class AnalisisSICController: Controller
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
            ViewBag.ModalTitulo = "Consultar análisis de información SIC";

            if(catalogosCargasRespuesta.ColAnios==null)
                return View("~/Views/Nacimientos/Archivos/Importar.cshtml", new ImportarArchivosViewModel());

            return View(catalogosCargasRespuesta);
        }
       

        public ActionResult AnalisisSICInformacion(string AniosJson,string MesesJson, string MpiosJson)
        {
            Servicio servicio = new Servicio();
            dynamic anios = JsonConvert.DeserializeObject(AniosJson);
            dynamic meses = JsonConvert.DeserializeObject(MesesJson);
            dynamic mpios = JsonConvert.DeserializeObject(MpiosJson);

            AnalisisSICPeticion peticion = new AnalisisSICPeticion
            {
                ColAnos = new Collection<string>()
            };
            foreach (string anio in anios)
            {
                peticion.ColAnos.Add(anio);                
            }

            peticion.ColMeses = new Collection<string>();
            foreach (string mes in meses)
            {
                peticion.ColMeses.Add(mes);
            }

            peticion.ColMunicipios = new Collection<Municipio>();            
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
            model.Totales = AnalisisSICRespuesta.DTs[0];
            model.RelacionPorFolio = AnalisisSICRespuesta.DTs[1];
            model.RelacionPorFecha = AnalisisSICRespuesta.DTs[2];
            model.Duplicados = AnalisisSICRespuesta.DTs[3];
            model.SinSinac = AnalisisSICRespuesta.DTs[4];

            //if (Request.IsAjaxRequest())
            return PartialView(model);
            
        }


    }
}