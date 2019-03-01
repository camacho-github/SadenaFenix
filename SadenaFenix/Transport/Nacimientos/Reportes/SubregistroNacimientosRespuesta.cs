using SadenaFenix.Models.Nacimientos.Consultas;
using SadenaFenix.Models.Nacimientos.Reportes;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Data;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Nacimientos.Reportes
{
    [DataContract]
    public class SubregistroNacimientosRespuesta
    {
        public SubregistroNacimientosRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }

        [DataMember(Name = "ColSubregistros", IsRequired = true)]
        [XmlAttribute("ColSubregistros")]
        public Collection<Subregistro> ColSubregistros { get; set; }

        [DataMember(Name = "ColOportunos", IsRequired = true)]
        [XmlAttribute("ColOportunos")]
        public Collection<Subregistro> ColOportunos { get; set; }

        [DataMember(Name = "ColExtemporaneos", IsRequired = true)]
        [XmlAttribute("ColExtemporaneos")]
        public Collection<Subregistro> ColExtemporaneos { get; set; }

        [DataMember(Name = "ColTotales", IsRequired = true)]
        [XmlAttribute("ColTotales")]
        public Collection<SubregistroTotal> ColTotales { get; set; }

        [DataMember(Name = "ColDataTables", IsRequired = true)]
        [XmlAttribute("ColDataTables")]
        public Collection<DataTable> ColDataTables { get; set; }

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


        [DataMember(Name = "ColCabeceros", IsRequired = true)]
        [XmlAttribute("ColCabeceros")]
        public Collection<string> ColCabeceros { get; set; }

        [DataMember(Name = "ColFilas", IsRequired = true)]
        [XmlAttribute("ColFilas")]
        public Collection<ReporteFila> ColFilas { get; set; }
    }
}
