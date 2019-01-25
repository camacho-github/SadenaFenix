using System.Collections.Generic;
using System;
using Sadena.Business.Nacimientos.Consultas;
using System.Web.Mvc;

namespace Sadena.Services.Nacimientos.Consultas
{
    public class ConsultasFacade
    {
        private readonly ConsultasBLL ConsultasBLL;

        public IList<SelectListItem> ObtenerAniosParaConsulta()
        {
            IList<SelectListItem> items = new List<SelectListItem>();
            IList<String> anios = ConsultasBLL.ObtenerAniosParaConsulta();
            foreach (String anio in anios)
            {
                items.Add(new SelectListItem { Value = anio, Text = anio });
            }
            return items;
        }
    }
}
