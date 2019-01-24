using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Catalogos.Socieconomica
{
    [DataContract]
    public class Sexo
    {
        [DataMember(Name = "SexoId", IsRequired = true)]
        [XmlAttribute("SexoId")]
        public int SexoId { get; set; }

        [DataMember(Name = "SexoDesc", IsRequired = true)]
        [XmlAttribute("SexoDesc")]
        public String SexoDesc { get; set; }
    }
}
