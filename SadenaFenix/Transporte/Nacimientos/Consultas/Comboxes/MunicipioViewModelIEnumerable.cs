using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Sadena.Transporte.Nacimientos.Consultas.Comboxes
{
    public class MunicipioViewModelIEnumerable
    {
        public IEnumerable<string> MunicipiosSeleccionados { get; set; }

        public List<SelectListItem> Municipios { get; } = new List<SelectListItem>();
    }
}
