using System.Collections.Generic;

namespace SadenaFenix.Transport.Nacimientos.Consultas
{
    public class ConsultarViewModel
    {
        public IEnumerable<string> MesesSeleccionados { get; set; }

        public IEnumerable<string> AniosSeleccionados { get; set; }

        public IEnumerable<string> MunicipiosSeleccionados { get; set; }

    }
}
