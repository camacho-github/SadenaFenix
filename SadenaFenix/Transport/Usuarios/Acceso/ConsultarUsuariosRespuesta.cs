using SadenaFenix.Models.Usuarios;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Data;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Usuarios.Acceso
{
    [DataContract]
    public class ConsultarUsuariosRespuesta
    {
        public ConsultarUsuariosRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }

        [DataMember(Name = "DTUsuarios", IsRequired = true)]
        [XmlAttribute("DTUsuarios")]
        public DataTable DTUsuarios { get; set; }

    }
}