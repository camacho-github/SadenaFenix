using System.Collections.Generic;
using SadenaFenix.Models.Catalogos.Geografia;
using System.Web.Mvc;
using SadenaFenix.Services;
using SadenaFenix.Transport.Catalogos;
using System.Collections.ObjectModel;
using SadenaFenix.Transport.Nacimientos.Consultas;
using SadenaFenix.Transport.Nacimientos.Consultas.Comboxes;
using SadenaFenix.Transport.Nacimientos.Reportes;

namespace SadenaFenix.Facade.Nacimientos.Consultas
{
    public class ConsultaFacade
    {
        /** Pre-consulta: cargar catálogos para combos. */
        public ConsultasViewModel ObtenerCalatogosParaConsulta()
        {
            Servicio servicio = new Servicio();
            ConsultasViewModel modelView = new ConsultasViewModel();
            /* Get catalogos. */
            CatalogosCargasRespuesta catalogosCargasRespuesta = servicio.ObtenerCatalogosCargas(null);
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
            modelView.ComboMeses = ComboMeses;
            modelView.ComboAnios = ComboAnios;
            modelView.ComboMunicipios = ComboMunicipios;
            return modelView;
        }

        /* Consulta información procesada de las tres categorias: subregistro, registrados y extemporáneos. */
        public SubregistroNacimientosRespuesta ConsultaSubregistroNacimientos(ConsultarViewModel consultarViewModel)
        {
            Servicio servicio = new Servicio();
            ConsultasViewModel modelView = new ConsultasViewModel();
            SubregistroPeticion solicitudDeConsulta = new SubregistroPeticion();
            /* Tomando datos de web. */
            Collection<string> ColAnos = new Collection<string>();
            Collection<string> ColMeses = new Collection<string>();
            Collection<Municipio> ColMunicipios = new Collection<Municipio>();
            /* Pasando valores. */
            foreach (string anio in consultarViewModel.AniosSeleccionados)
            {
                ColAnos.Add(anio);
            }

            foreach (string mes in consultarViewModel.MesesSeleccionados)
            {
                ColMeses.Add(mes);
            }

            foreach (string municipioId in consultarViewModel.MunicipiosSeleccionados)
            {
                Municipio municipio = new Municipio();
                municipio.MpioId = int.Parse(municipioId);
                ColMunicipios.Add(municipio);
            }
            solicitudDeConsulta.ColAnos = ColAnos;
            solicitudDeConsulta.ColMeses = ColMeses;
            solicitudDeConsulta.ColMunicipios = ColMunicipios;
            /* Obtiene resultados de la consulta. */
            SubregistroNacimientosRespuesta respuestaDeConsulta = servicio.ConsultaSubregistroNacimientos(solicitudDeConsulta);
            return respuestaDeConsulta;
        }

        /* Consulta los totales con porcentaje de la información procesada de las tres categorias: subregistro, registrados y extemporáneos. */
        public TotalesSubregistroNacimientosRespuesta ConsultaTotalesSubregistroNacimientos()
        {
            Servicio servicio = new Servicio();
            ConsultasViewModel modelView = new ConsultasViewModel();
            SubregistroPeticion solicitudDeConsulta = new SubregistroPeticion();
            /* Tomando datos de web. */
            Collection<string> ColAnos = new Collection<string>();
            Collection<string> ColMeses = new Collection<string>();
            Collection<Municipio> ColMunicipios = new Collection<Municipio>();
            /* Pasando valores. */
            solicitudDeConsulta.ColAnos = ColAnos;
            solicitudDeConsulta.ColMeses = ColMeses;
            solicitudDeConsulta.ColMunicipios = ColMunicipios;
            /* Obtenener totales de la información procesada. */
            TotalesSubregistroNacimientosRespuesta respuestaTotalesDelSubregistro = servicio.ConsultaTotalesSubregistroNacimientos(solicitudDeConsulta);
            return respuestaTotalesDelSubregistro;
        }

    }

}
