using SadenaFenix.Models.Nacimientos.Consultas;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Nacimientos.Reportes
{
    [DataContract]
    public class TotalesMunicipiosRespuesta
    {
        public TotalesMunicipiosRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }

        [DataMember(Name = "ColTotales", IsRequired = true)]
        [XmlAttribute("ColTotales")]
        public Collection<TotalesMunicipio> ColTotales { get; set; }

        [DataMember(Name = "JsonTotales", IsRequired = true)]
        [XmlAttribute("JsonTotales")]
        public string JsonTotales { get; set; }

    }
}
