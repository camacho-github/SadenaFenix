using SadenaFenix.Models.Georeferenciacion;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Data;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Georeferenciacion
{
    [DataContract]
    public class ConsultaOficialiaRespuesta
    {
        public ConsultaOficialiaRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }

        [DataMember(Name = "ColOficialia", IsRequired = true)]
        [XmlAttribute("ColOficialia")]
        public Collection<Oficialia> ColOficialia { get; set; }

        [DataMember(Name = "DTOficialia", IsRequired = true)]
        [XmlAttribute("DTOficialia")]
        public DataTable DTOficialia { get; set; }

    }
}