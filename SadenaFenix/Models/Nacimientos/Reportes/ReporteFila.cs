using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Nacimientos.Reportes
{
    [DataContract]
    public class ReporteFila
    {
        [DataMember(Name = "ColCeldas", IsRequired = true)]
        [XmlAttribute("ColCeldas")]
        public Collection<ReporteCelda> ColCeldas { get; set; }
    }
}