using SadenaFenix.Models.Usuarios;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Usuarios.Acceso
{
    [DataContract]
    public class InsertarUsuarioPeticion
    {
        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroPeticion Cabecero { get; set; }

        [DataMember(Name = "Usuario", IsRequired = true)]
        [XmlAttribute("Usuario")]
        public UsuarioAlta Usuario { get; set; }
    }
}
