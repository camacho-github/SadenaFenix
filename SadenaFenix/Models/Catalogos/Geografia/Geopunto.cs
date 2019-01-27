using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Catalogos.Geografia
{
    [DataContract]
    public class Geopunto
    {
        [DataMember(Name = "lat", IsRequired = true)]
        [XmlAttribute("lat")]
        public double Latitud { get; set; }

        [DataMember(Name = "lng", IsRequired = true)]
        [XmlAttribute("lng")]
        public double Longitud { get; set; }

    }
}