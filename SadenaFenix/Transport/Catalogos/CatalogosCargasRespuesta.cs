using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Models.Catalogos.Tiempo;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Catalogos
{
    [DataContract]
    public class CatalogosCargasRespuesta
    {
        public CatalogosCargasRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }

        [DataMember(Name = "ColAnios", IsRequired = true)]
        [XmlAttribute("ColAnios")]
        public Collection<Anio> ColAnios { get; set; }

        [DataMember(Name = "Anio", IsRequired = true)]
        [XmlAttribute("Anio")]
        public Anio Ano { get; set; }

        [DataMember(Name = "ColMeses", IsRequired = true)]
        [XmlAttribute("ColMeses")]
        public Collection<Mes> ColMeses { get; set; }

        [DataMember(Name = "Mes", IsRequired = true)]
        [XmlAttribute("Mes")]
        public Mes Mes { get; set; }

        [DataMember(Name = "ColMunicipios", IsRequired = true)]
        [XmlAttribute("ColMunicipios")]
        public Collection<Municipio> ColMunicipios { get; set; }

        [DataMember(Name = "Municipio", IsRequired = true)]
        [XmlAttribute("Municipio")]
        public Municipio Municipio { get; set; }

    }
}
