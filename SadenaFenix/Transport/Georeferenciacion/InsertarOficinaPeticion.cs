using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Models.Georeferenciacion;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Georeferenciacion
{
    [DataContract]
    public class InsertarOficinaPeticion
    {
        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroPeticion Cabecero { get; set; }

        [DataMember(Name = "Oficina", IsRequired = true)]
        [XmlAttribute("Oficina")]
        public Oficina Oficina { get; set; }

        [DataMember(Name = "UserJson", IsRequired = true)]
        [XmlAttribute("UserJson")]
        public string UserJson { get; set; }
    }
}