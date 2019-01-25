using SadenaFenix.Models.Catalogos.Tiempo;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Catalogos
{
    [DataContract]
    public class ConsultaMesesRespuesta
    {
        public ConsultaMesesRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }

        [DataMember(Name = "ColMeses", IsRequired = true)]
        [XmlAttribute("ColMeses")]
        public Collection<Mes> ColMeses { get; set; }
    }
}