using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Catalogos.Tiempo
{
    [DataContract]
    public class Anio
    {
        [DataMember(Name = "AnioId", IsRequired = true)]
        [XmlAttribute("AnioId")]
        public int AnioId { get; set; }

        [DataMember(Name = "AnioDesc", IsRequired = true)]
        [XmlAttribute("AnioDesc")]
        public string AnioDesc { get; set; }

    }
}