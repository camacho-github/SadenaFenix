using SadenaFenix.Models.Usuarios;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Usuarios.Acceso
{
    [DataContract]
    public class ConsultarUsuarioPeticion
    {
        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroPeticion Cabecero { get; set; }

        [DataMember(Name = "UsuarioId", IsRequired = true)]
        [XmlAttribute("UsuarioId")]
        public int UsuarioId { get; set; }

    }
}
