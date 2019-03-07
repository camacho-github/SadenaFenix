using SadenaFenix.Models.Georeferenciacion;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Data;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Georeferenciacion
{
    [DataContract]
    public class ConsultarOficinaRespuesta
    {
        public ConsultarOficinaRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }

        [DataMember(Name = "Oficina", IsRequired = true)]
        [XmlAttribute("Oficina")]
        public Oficina Oficina { get; set; }
    }
}