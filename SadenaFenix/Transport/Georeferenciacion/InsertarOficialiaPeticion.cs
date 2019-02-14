using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Models.Georeferenciacion;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Georeferenciacion
{
    [DataContract]
    public class InsertarOficialiaPeticion
    {
        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroPeticion Cabecero { get; set; }

        [DataMember(Name = "Oficialia", IsRequired = true)]
        [XmlAttribute("Oficialia")]
        public Oficialia Oficialia { get; set; }
    }
}