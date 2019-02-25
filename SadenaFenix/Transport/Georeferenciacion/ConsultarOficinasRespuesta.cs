using SadenaFenix.Models.Georeferenciacion;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Data;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Georeferenciacion
{
    [DataContract]
    public class ConsultarOficinasRespuesta
    {
        public ConsultarOficinasRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }

        [DataMember(Name = "ColOficinas", IsRequired = true)]
        [XmlAttribute("ColOficinas")]
        public Collection<Oficina> ColOficinas { get; set; }

        [DataMember(Name = "DTOficinas", IsRequired = true)]
        [XmlAttribute("DTOficinas")]
        public DataTable DTOficinas { get; set; }

        [DataMember(Name = "UserJson", IsRequired = true)]
        [XmlAttribute("UserJson")]
        public string UserJson { get; set; }

    }
}