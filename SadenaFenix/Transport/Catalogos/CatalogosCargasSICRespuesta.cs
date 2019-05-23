using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Models.Catalogos.Tiempo;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Catalogos
{
    [DataContract]
    public class CatalogosCargasSICRespuesta
    {
        public CatalogosCargasSICRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }

        [DataMember(Name = "ColAniosNac", IsRequired = true)]
        [XmlAttribute("ColAniosNac")]
        public Collection<Anio> ColAniosNac { get; set; }

        [DataMember(Name = "ColAniosRegistro", IsRequired = true)]
        [XmlAttribute("ColAniosRegistro")]
        public Collection<Anio> ColAniosRegistro { get; set; }

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
