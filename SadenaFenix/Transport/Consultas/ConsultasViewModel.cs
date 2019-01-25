using Sadena.Transport.Nacimientos.Consultas.Comboxes;

namespace Sadena.Transport.Nacimientos.Consultas
{
    public class ConsultasViewModel
    {
        public MesViewModelIEnumerable ComboMeses { get; set; }

        public AnioViewModelIEnumerable ComboAnios { get; set; }

        public MunicipioViewModelIEnumerable ComboMunicipios { get; set; }
    }
}
