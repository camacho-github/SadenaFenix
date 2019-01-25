using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Sadena.Transporte.Usuarios.Acceso
{
    [DataContract]
    public class SesionPeticion
    {
        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroPeticion Cabecero { get; set; }

        [DataMember(Name = "Identificador", IsRequired = true)]
        [XmlAttribute("Identificador")]
        public string Identificador { get; set; }

        [DataMember(Name = "Contrasena", IsRequired = true)]
        [XmlAttribute("Contrasena")]
        public string Contrasena { get; set; }

        [DataMember(Name = "IP", IsRequired = true)]
        [XmlAttribute("IP")]
        public string IP { get; set; }

    }
}
