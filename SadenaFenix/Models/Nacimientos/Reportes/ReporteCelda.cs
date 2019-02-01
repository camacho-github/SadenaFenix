using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Nacimientos.Reportes
{
    [DataContract]
    public class ReporteCelda
    {
        [DataMember(Name = "NombreCelda", IsRequired = true)]
        [XmlAttribute("NombreCelda")]
        public string NombreCelda { get; set; }

        [DataMember(Name = "Valor", IsRequired = true)]
        [XmlAttribute("Valor")]
        public String Valor { get; set; }
    }
}