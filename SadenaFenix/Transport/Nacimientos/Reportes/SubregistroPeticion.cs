using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Nacimientos.Reportes
{
    [DataContract]
    public class SubregistroPeticion
    {
        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroPeticion Cabecero { get; set; }

        [DataMember(Name = "ColAnos", IsRequired = true)]
        [XmlAttribute("ColAnos")]
        public Collection<string> ColAnos { get; set; }

        [DataMember(Name = "ColMeses", IsRequired = true)]
        [XmlAttribute("ColMeses")]
        public Collection<string> ColMeses { get; set; }

        [DataMember(Name = "ColMunicipios", IsRequired = true)]
        [XmlAttribute("ColMunicipios")]
        public Collection<Municipio> ColMunicipios { get; set; }
    }
}
