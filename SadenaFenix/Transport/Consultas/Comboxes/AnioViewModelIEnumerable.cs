using System.Collections.Generic;
using System.Web.Mvc;

namespace Sadena.Transport.Nacimientos.Consultas.Comboxes
{
    public class AnioViewModelIEnumerable
    {
        public IEnumerable<string> AniosSeleccionados { get; set; }

        public List<SelectListItem> Anios { get; } = new List<SelectListItem>();

    }
}
