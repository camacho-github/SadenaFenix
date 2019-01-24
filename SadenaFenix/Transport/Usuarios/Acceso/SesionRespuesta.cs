using SadenaFenix.Models.Usuarios;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Usuarios.Acceso
{
    [DataContract]
    public class SesionRespuesta
    {
        public SesionRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }

        [DataMember(Name = "Usuario", IsRequired = true)]
        [XmlAttribute("Usuario")]
        public Usuario Usuario { get; set; }
    }
}
