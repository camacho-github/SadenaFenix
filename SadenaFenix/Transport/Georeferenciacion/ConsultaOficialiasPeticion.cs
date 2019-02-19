using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Georeferenciacion
{
    [DataContract]
    public class ConsultaOficialiasPeticion
    {
        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroPeticion Cabecero { get; set; }

        [DataMember(Name = "ColMunicipios", IsRequired = true)]
        [XmlAttribute("ColMunicipios")]
        public Collection<Municipio> ColMunicipios { get; set; }
    }
}