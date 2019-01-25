using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Sadena.Transporte.Nacimientos.Consultas.Comboxes
{
    public class AnioViewModelIEnumerable
    {
        public IEnumerable<string> AniosSeleccionados { get; set; }

        public List<SelectListItem> Anios { get; } = new List<SelectListItem>();

    }
}
