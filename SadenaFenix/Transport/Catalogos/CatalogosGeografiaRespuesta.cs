using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Catalogos
{
    [DataContract]
    public class CatalogosGeografiaRespuesta
    {
        public CatalogosGeografiaRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }
       
        [DataMember(Name = "colMunicipio", IsRequired = true)]
        [XmlAttribute("colMunicipio")]
        public Collection<Municipio> ColMunicipio { get; set; }

        [DataMember(Name = "colLocalidad", IsRequired = true)]
        [XmlAttribute("colLocalidad")]
        public Collection<Localidad> ColLocalidad { get; set; }

    }
}