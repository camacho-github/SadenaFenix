using System.Collections.Generic;
using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Business.Catalogos.Geografia;
using System.Web.Mvc;

namespace Sadena.Sevices.Catalogos.Geografia
{
    public class CatMunicipioFacade
    {
        private readonly CatMunicipioBusiness CatMunicipioBusiness;

        public List<SelectListItem> ObtenerTodosLosMuncipios()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            List<Municipio> municipios = CatMunicipioBusiness.ObtenerTodosLosMuncipios();
            foreach (Municipio municipio in municipios)
            {
                items.Add(new SelectListItem { Value = "0" + municipio.MpioId, Text = municipio.MpioDesc });
            }
            return items;
        }

    }
}
