using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sadena.Models.Catalogos.Geografia;
using Sadena.Business.Catalogos.Geografia;

namespace Sadena.Facade.Catalogos.Geografia
{
    public class CatMunicipioFacade
    {
        private readonly CatMunicipioBusiness CatMunicipioBusiness;

        public IList<SelectListItem> ObtenerTodosLosMuncipios()
        {
            IList<SelectListItem> items = new List<SelectListItem>();
            IList<Municipio> municipios = CatMunicipioBusiness.ObtenerTodosLosMuncipios();
            foreach (Municipio municipio in municipios)
            {
                items.Add(new SelectListItem { Value = "0" + municipio.MpioId, Text = municipio.MpioDesc });
            }
            return items;
        }

    }
}
