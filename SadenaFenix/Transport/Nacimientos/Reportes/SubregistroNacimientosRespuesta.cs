using SadenaFenix.Models.Nacimientos.Consultas;
using SadenaFenix.Models.Nacimientos.Reportes;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Nacimientos.Reportes
{
    [DataContract]
    public class SubregistroNacimientosRespuesta
    {
        public SubregistroNacimientosRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }

        [DataMember(Name = "ColSubregistros", IsRequired = true)]
        [XmlAttribute("ColSubregistros")]
        public Collection<Subregistro> ColSubregistros { get; set; }

        [DataMember(Name = "ColOportunos", IsRequired = true)]
        [XmlAttribute("ColOportunos")]
        public Collection<Subregistro> ColOportunos { get; set; }

        [DataMember(Name = "ColExtemporaneos", IsRequired = true)]
        [XmlAttribute("ColExtemporaneos")]
        public Collection<Subregistro> ColExtemporaneos { get; set; }

        [DataMember(Name = "ColTotales", IsRequired = true)]
        [XmlAttribute("ColTotales")]
        public Collection<SubregistroTotal> ColTotales { get; set; }
    }
}
