using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Catalogos.Geografia
{
    [DataContract]
    public class Localidad
    {
        [DataMember(Name = "LocEdoId", IsRequired = true)]
        [XmlAttribute("LocEdoId")]
        public int LocEdoId { get; set; }

        [DataMember(Name = "LocEdoDesc", IsRequired = true)]
        [XmlAttribute("LocEdoDesc")]
        public String LocEdoDesc { get; set; }

        [DataMember(Name = "LocMpioId", IsRequired = true)]
        [XmlAttribute("LocMpioId")]
        public int LocMpioId { get; set; }

        [DataMember(Name = "LocMpioDesc", IsRequired = true)]
        [XmlAttribute("LocMpioDesc")]
        public String LocMpioDesc { get; set; }

        [DataMember(Name = "LocId", IsRequired = true)]
        [XmlAttribute("LocId")]
        public int LocId { get; set; }

        [DataMember(Name = "LocDesc", IsRequired = true)]
        [XmlAttribute("LocDesc")]
        public String LocDesc { get; set; }


    }
}
