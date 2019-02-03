using System.Collections.Generic;
using SadenaFenix.Models.Catalogos.Geografia;
using System.Web.Mvc;
using SadenaFenix.Services;
using SadenaFenix.Transport.Catalogos;
using System.Collections.ObjectModel;
using SadenaFenix.Transport.Nacimientos.Consultas;
using SadenaFenix.Transport.Nacimientos.Consultas.Comboxes;

namespace SadenaFenix.Facade.Nacimientos.Consultas
{
    public class ConsultaFacade
    {
        /*private readonly CatMunicipioBusiness CatMunicipioBusiness;*/
        /*private readonly ConsultasBLL ConsultasBLL;*/
        public ConsultasViewModel ObtenerTodosLosMuncipios()
        {
            Servicio Servicio = new Servicio();
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
            MesViewModelIEnumerable ComboMeses = new MesViewModelIEnumerable();
            AnioViewModelIEnumerable ComboAnios = new AnioViewModelIEnumerable();
            MunicipioViewModelIEnumerable ComboMunicipios = new MunicipioViewModelIEnumerable();
            ComboMeses.Meses.AddRange(mesesItems);
            ComboAnios.Anios.AddRange(aniosItems);
            ComboMunicipios.Municipios.AddRange(municipiosItems);
            model.ComboMeses = ComboMeses;
            model.ComboAnios = ComboAnios;
            model.ComboMunicipios = ComboMunicipios;
            return model;
        }

    }

}
