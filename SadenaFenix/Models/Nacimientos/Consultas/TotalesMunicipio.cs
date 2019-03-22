using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Nacimientos.Consultas
{
    [DataContract]
    public class TotalesMunicipio
    {
        [DataMember(Name = "IdMunicipio", IsRequired = true)]
        [XmlAttribute("IdMunicipio")]
        public int IdMunicipio { get; set; }

        [DataMember(Name = "TotalSubregistro", IsRequired = true)]
        [XmlAttribute("TotalSubregistro")]
        public int TotalSubregistro { get; set; }

        [DataMember(Name = "TotalRegistroOportuno", IsRequired = true)]
        [XmlAttribute("TotalRegistroOportuno")]
        public int TotalRegistroOportuno { get; set; }

        [DataMember(Name = "TotalRegistroExtemporaneo", IsRequired = true)]
        [XmlAttribute("TotalRegistroExtemporaneo")]
        public int TotalRegistroExtemporaneo { get; set; }

    }
}
