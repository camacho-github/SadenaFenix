using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Catalogos.Socioeconomica
{
    [DataContract]
    public class EstatusRegistro
    {
        [DataMember(Name = "EstatusRegistroId", IsRequired = true)]
        [XmlAttribute("EstatusRegistroId")]
        public int EstatusRegistroId { get; set; }

        [DataMember(Name = "EstatusRegistroDesc", IsRequired = true)]
        [XmlAttribute("EstatusRegistroDesc")]
        public String EstatusRegistroDesc { get; set; }
    }
}