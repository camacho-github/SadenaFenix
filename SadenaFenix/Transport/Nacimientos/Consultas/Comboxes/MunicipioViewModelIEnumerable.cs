﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace SadenaFenix.Transport.Nacimientos.Consultas.Comboxes
{
    public class MunicipioViewModelIEnumerable
    {
        public IEnumerable<string> MunicipiosSeleccionados { get; set; }

        public List<SelectListItem> Municipios { get; } = new List<SelectListItem>();
    }
}
