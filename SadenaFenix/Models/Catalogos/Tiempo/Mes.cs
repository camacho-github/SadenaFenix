using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Catalogos.Tiempo
{
    [DataContract]
    public class Mes
    {
        [DataMember(Name = "MesId", IsRequired = true)]
        [XmlAttribute("MesId")]
        public int MesId { get; set; }

        [DataMember(Name = "MesDesc", IsRequired = true)]
        [XmlAttribute("MesDesc")]
        public string MesDesc { get; set; }

    }
}