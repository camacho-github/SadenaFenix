using SadenaFenix.Models.Nacimientos.Reportes;
using SadenaFenix.Transport.Usuarios.Acceso;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Nacimientos.Reportes
{
    [DataContract]
    public class ReporteSubregistroRespuesta
    {
        public ReporteSubregistroRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }

        [DataMember(Name = "ColCabeceros", IsRequired = true)]
        [XmlAttribute("ColCabeceros")]
        public Collection<string> ColCabeceros { get; set; }

        [DataMember(Name = "ColFilas", IsRequired = true)]
        [XmlAttribute("ColFilas")]
        public Collection<ReporteFila> ColFilas { get; set; }

        [DataMember(Name = "XmlReporte", IsRequired = true)]
        [XmlAttribute("XmlReporte")]
        public string XmlReporte { get; set; }


        [DataMember(Name = "Cabeceros", IsRequired = true)]
        [XmlAttribute("Cabeceros")]
        public Collection<Collection<string>> Cabeceros{ get; set; }


        [DataMember(Name = "DTs", IsRequired = true)]
        [XmlAttribute("DTs")]
        public Collection<DataTable> DTs { get; set; }
      

    }
}