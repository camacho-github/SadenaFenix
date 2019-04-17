using SadenaFenix.Models.Usuarios;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Usuarios.Acceso
{
    [DataContract]
    public class ActualizarUsuarioRespuesta
    {
        public ActualizarUsuarioRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }
    }
}
