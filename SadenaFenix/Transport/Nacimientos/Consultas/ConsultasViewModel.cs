using SadenaFenix.Models.Usuarios;
using SadenaFenix.Transport.Nacimientos.Consultas.Comboxes;

namespace SadenaFenix.Transport.Nacimientos.Consultas
{
    public class ConsultasViewModel
    {

        public MesViewModelIEnumerable ComboMeses { get; set; }

        public AnioViewModelIEnumerable ComboAnios { get; set; }

        public MunicipioViewModelIEnumerable ComboMunicipios { get; set; }

        public Usuario Usuario { get; set; }
    }
}
