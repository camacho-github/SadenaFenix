using System.Collections.Generic;
using SadenaFenix.Models.Catalogos.Geografia;
using System.Web.Mvc;
using SadenaFenix.Services;
using SadenaFenix.Transport.Catalogos;
using System.Collections.ObjectModel;
using SadenaFenix.Transport.Nacimientos.Consultas;

namespace Sadena.Sevices.Catalogos.Geografia
{
    public class CatMunicipioFacade
    {
        /*private readonly CatMunicipioBusiness CatMunicipioBusiness;*/
        private Servicio Servicio;

        public ConsultasViewModel ObtenerTodosLosMuncipios()
        {
            ConsultasViewModel model = new ConsultasViewModel();
            /* Get catalogos. */
            CatalogosCargasRespuesta catalogosCargasRespuesta = Servicio.ObtenerCatalogosCargas(null);
            Collection<string> anios = catalogosCargasRespuesta.ColAnos;
            Collection<string> meses = catalogosCargasRespuesta.ColMeses;
            Collection<Municipio> municipios = catalogosCargasRespuesta.ColMunicipios;

            /* Municipios */
            List<SelectListItem> municipiosItems = new List<SelectListItem>();
            foreach (Municipio municipio in municipios)
            {
                municipiosItems.Add(new SelectListItem { Value = "" + municipio.MpioId, Text = municipio.MpioDesc });
            }
            /* Meses */
            List<SelectListItem> mesesItems = new List<SelectListItem>();
            foreach (string mes in meses)
            {
                mesesItems.Add(new SelectListItem { Value = mes, Text = mes });
            }
            List<SelectListItem> aniosItems = new List<SelectListItem>();
            foreach (string anio in anios)
            {
                aniosItems.Add(new SelectListItem { Value = anio, Text = anio });
            }
            model.ComboMunicipios.Municipios.AddRange(municipiosItems);
            model.ComboMeses.Meses.AddRange(mesesItems);
            model.ComboAnios.Anios.AddRange(aniosItems);
            return model;
        }

    }
}
