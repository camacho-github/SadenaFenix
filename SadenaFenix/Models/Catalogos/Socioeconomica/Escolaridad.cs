using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Catalogos.Socieconomica
{
    [DataContract]
    public class Escolaridad
    {
        [DataMember(Name = "EscolaridadId", IsRequired = true)]
        [XmlAttribute("EscolaridadId")]
        public int EscolaridadId { get; set; }

        [DataMember(Name = "EscolaridadDesc", IsRequired = true)]
        [XmlAttribute("EscolaridadDesc")]
        public String EscolaridadDesc { get; set; }
    }
}
