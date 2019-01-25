using Sadena.Transporte.Nacimientos.Consultas.Comboxes;

namespace Sadena.Transporte.Nacimientos.Consultas
{
    public class ConsultasViewModel
    {
        public MesViewModelIEnumerable ComboMeses { get; set; }

        public AnioViewModelIEnumerable ComboAnios { get; set; }

        public MunicipioViewModelIEnumerable ComboMunicipios { get; set; }
    }
}
