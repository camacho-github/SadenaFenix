using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Nacimientos.Consultas
{
    [DataContract]
    public class SubregistroTotal
    {
        [DataMember(Name = "NombreGrupo", IsRequired = true)]
        [XmlAttribute("NombreGrupo")]
        public String NombreGrupo { get; set; }

        [DataMember(Name = "Total", IsRequired = true)]
        [XmlAttribute("Total")]
        public int Total { get; set; }

    }
}
