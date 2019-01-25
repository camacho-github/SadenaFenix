using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Catalogos.Geografia
{
    [DataContract]
    public class Municipio
    {
        public Municipio(int mpioId, string mpioDesc)
        {
            MpioId = mpioId;
            MpioDesc = mpioDesc;
        }

        [DataMember(Name = "MpioId", IsRequired = true)]
        [XmlAttribute("MpioId")]
        public int MpioId { get; set; }

        [DataMember(Name = "MpioDesc", IsRequired = true)]
        [XmlAttribute("MpioDesc")]
        public String MpioDesc { get; set; }

    }
}
