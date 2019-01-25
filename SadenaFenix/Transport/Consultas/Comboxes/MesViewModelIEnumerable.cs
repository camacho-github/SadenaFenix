using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Sadena.Transporte.Nacimientos.Consultas
{
    public class MesViewModelIEnumerable
    {
        public IEnumerable<string> MesesSeleccionados { get; set; }

        public List<SelectListItem> Meses { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "01", Text = "Enero" },
            new SelectListItem { Value = "02", Text = "Febrero" },
            new SelectListItem { Value = "03", Text = "Marzo"    },
            new SelectListItem { Value = "04", Text = "Abril" },
            new SelectListItem { Value = "05", Text = "Mayo"  },
            new SelectListItem { Value = "06", Text = "Junio"},
            new SelectListItem { Value = "07", Text = "Julio" },
            new SelectListItem { Value = "08", Text = "Agosto"  },
            new SelectListItem { Value = "09", Text = "Septiembre" },
            new SelectListItem { Value = "10", Text = "Octubre"  },
            new SelectListItem { Value = "11", Text = "Noviembre" },
            new SelectListItem { Value = "12", Text = "Diciembre"  },
         };
    }
}
