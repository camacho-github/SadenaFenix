using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Catalogos.Geografia
{
    [DataContract]
    public class Municipio
    {
        public Municipio()
        {

        }

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

        [DataMember(Name = "Latitud", IsRequired = true)]
        [XmlAttribute("Latitud")]
        public String Latitud { get; set; }

        [DataMember(Name = "Longitud", IsRequired = true)]
        [XmlAttribute("Longitud")]
        public String Longitud { get; set; }

        [DataMember(Name = "JsonPoligono", IsRequired = true)]
        [XmlAttribute("JsonPoligono")]
        public String JsonPoligono { get; set; }

    }
}
