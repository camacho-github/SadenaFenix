using SadenaFenix.Models.Nacimientos.Consultas;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Nacimientos.Reportes
{
    [DataContract]
    public class TotalesSubregistroNacimientosRespuesta
    {
        public TotalesSubregistroNacimientosRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }

        [DataMember(Name = "ColTotales", IsRequired = true)]
        [XmlAttribute("ColTotales")]
        public Collection<SubregistroTotal> ColTotales { get; set; }

        [DataMember(Name = "Total", IsRequired = true)]
        [XmlAttribute("Total")]
        public int Total { get; set; }

        [DataMember(Name = "TotalSubregistro", IsRequired = true)]
        [XmlAttribute("TotalSubregistro")]
        public int TotalSubregistro { get; set; }

        [DataMember(Name = "PorcentajeSubregistro", IsRequired = true)]
        [XmlAttribute("PorcentajeSubregistro")]
        public decimal PorcentajeSubregistro { get; set; }

        [DataMember(Name = "TotalRegistroOportuno", IsRequired = true)]
        [XmlAttribute("TotalRegistroOportuno")]
        public int TotalRegistroOportuno { get; set; }

        [DataMember(Name = "PorcentajeRegistroOportuno", IsRequired = true)]
        [XmlAttribute("PorcentajeRegistroOportuno")]
        public decimal PorcentajeRegistroOportuno { get; set; }

        [DataMember(Name = "TotalRegistroExtemporaneo", IsRequired = true)]
        [XmlAttribute("TotalRegistroExtemporaneo")]
        public int TotalRegistroExtemporaneo { get; set; }

        [DataMember(Name = "PorcentajeRegistroExtemporaneo", IsRequired = true)]
        [XmlAttribute("PorcentajeRegistroExtemporaneo")]
        public decimal PorcentajeRegistroExtemporaneo { get; set; }
    }
}
