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
    public class TotalesCoverturaRegistral
    {
        public TotalesCoverturaRegistral()
        {
            Cabecero = new CabeceroRespuesta();
        }


        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }

        [DataMember(Name = "TotalOportunoRelacionPorFolio", IsRequired = true)]
        [XmlAttribute("TotalOportunoRelacionPorFolio")]
        public int TotalOportunoRelacionPorFolio { get; set; }

        [DataMember(Name = "PorcentajeOportunoRelacionPorFolio", IsRequired = true)]
        [XmlAttribute("PorcentajeOportunoRelacionPorFolio")]
        public decimal PorcentajeOportunoRelacionPorFolio { get; set; }

        [DataMember(Name = "TotalOportunoRelacionPorFecha", IsRequired = true)]
        [XmlAttribute("TotalOportunoRelacionPorFecha")]
        public int TotalOportunoRelacionPorFecha { get; set; }

        [DataMember(Name = "PorcentajeOportunoRelacionPorFecha", IsRequired = true)]
        [XmlAttribute("PorcentajeOportunoRelacionPorFecha")]
        public decimal PorcentajeOportunoRelacionPorFecha { get; set; }


        [DataMember(Name = "TotalExtemporaneoRelacionPorFolio", IsRequired = true)]
        [XmlAttribute("TotalExtemporaneoRelacionPorFolio")]
        public int TotalExtemporaneoRelacionPorFolio { get; set; }

        [DataMember(Name = "PorcentajeExtemporaneoRelacionPorFolio", IsRequired = true)]
        [XmlAttribute("PorcentajeExtemporaneoRelacionPorFolio")]
        public decimal PorcentajeExtemporaneoRelacionPorFolio { get; set; }

        [DataMember(Name = "TotalExtemporaneoRelacionPorFecha", IsRequired = true)]
        [XmlAttribute("TotalExtemporaneoRelacionPorFecha")]
        public int TotalExtemporaneoRelacionPorFecha { get; set; }

        [DataMember(Name = "TotalRegistrosCompilados", IsRequired = true)]
        [XmlAttribute("TotalRegistrosCompilados")]
        public int TotalRegistrosCompilados { get; set; }

        [DataMember(Name = "PorcentajeRegistrosCompilados", IsRequired = true)]
        [XmlAttribute("PorcentajeRegistrosCompilados")]
        public decimal PorcentajeRegistrosCompilados { get; set; }

        [DataMember(Name = "PorcentajeExtemporaneoRelacionPorFecha", IsRequired = true)]
        [XmlAttribute("PorcentajeExtemporaneoRelacionPorFecha")]
        public decimal PorcentajeExtemporaneoRelacionPorFecha { get; set; }

        [DataMember(Name = "TotalOportunoSinRelacion", IsRequired = true)]
        [XmlAttribute("TotalOportunoSinRelacion")]
        public int TotalOportunoSinRelacion { get; set; }

        [DataMember(Name = "PorcentajeOportunoSinRelacion", IsRequired = true)]
        [XmlAttribute("PorcentajeOportunoSinRelacion")]
        public decimal PorcentajeOportunoSinRelacion { get; set; }

        [DataMember(Name = "TotalExtemporaneoSinRelacion", IsRequired = true)]
        [XmlAttribute("TotalExtemporaneoSinRelacion")]
        public int TotalExtemporaneoSinRelacion { get; set; }

        [DataMember(Name = "PorcentajeExtemporaneoSinRelacion", IsRequired = true)]
        [XmlAttribute("PorcentajeExtemporaneoSinRelacion")]
        public decimal PorcentajeExtemporaneoSinRelacion { get; set; }

        [DataMember(Name = "TotalRegistrosSinRelacion", IsRequired = true)]
        [XmlAttribute("TotalRegistrosSinRelacion")]
        public int TotalRegistrosSinRelacion { get; set; }

        [DataMember(Name = "PorcentajeRegistrosSinRelacion", IsRequired = true)]
        [XmlAttribute("PorcentajeRegistrosSinRelacion")]
        public decimal PorcentajeRegistrosSinRelacion { get; set; }

        [DataMember(Name = "SumatoriaTotal", IsRequired = true)]
        [XmlAttribute("SumatoriaTotal")]
        public int SumatoriaTotal { get; set; }

        [DataMember(Name = "PorcentajeTotal", IsRequired = true)]
        [XmlAttribute("PorcentajeTotal")]
        public decimal PorcentajeTotal { get; set; }


    }
}
