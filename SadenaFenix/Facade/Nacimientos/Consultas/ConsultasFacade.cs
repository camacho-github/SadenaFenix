using Microsoft.AspNetCore.Mvc.Rendering;
using Sadena.Models.Catalogos.Geografia;
using System.Collections.Generic;
using Sadena.Business.Nacimientos.Consultas;
using System;

namespace Sadena.Facade.Nacimientos.Consultas
{
    public class ConsultasFacade
    {
        private readonly ConsultasBusiness ConsultasBusiness;

        public IList<SelectListItem> ObtenerAniosParaConsulta()
        {
            IList<SelectListItem> items = new List<SelectListItem>();
            IList<String> anios = ConsultasBusiness.ObtenerAniosParaConsulta();
            foreach (String anio in anios)
            {
                items.Add(new SelectListItem { Value = anio, Text = anio });
            }
            return items;
        }
    }
}
