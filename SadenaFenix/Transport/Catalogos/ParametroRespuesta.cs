using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Models.Catalogos.Tiempo;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Catalogos
{
    [DataContract]
    public class ParametroRespuesta
    {
        public ParametroRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }

        [DataMember(Name = "ParametroValor", IsRequired = true)]
        [XmlAttribute("ParametroValor")]
        public int ParametroValor { get; set; }

    }
}
