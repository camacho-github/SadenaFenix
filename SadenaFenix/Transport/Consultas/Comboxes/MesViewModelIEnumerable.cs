using System.Collections.Generic;
using System.Web.Mvc;

namespace SadenaFenix.Transport.Nacimientos.Consultas.Comboxes
{
    public class MesViewModelIEnumerable
    {
        public IEnumerable<string> MesesSeleccionados { get; set; }

        public List<SelectListItem> Meses { get; } = new List<SelectListItem>();
    }
}
