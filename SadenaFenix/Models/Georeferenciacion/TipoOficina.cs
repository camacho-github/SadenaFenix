using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Georeferenciacion
{
    [DataContract]
    public class TipoOficina
    {
        public TipoOficina()
        {

        }

        public TipoOficina(int tipoId, string tipoDesc)
        {
            TipoId = tipoId;
            TipoDesc = tipoDesc;
        }

        [DataMember(Name = "TipoId", IsRequired = true)]
        [XmlAttribute("TipoId")]
        public int TipoId { get; set; }

        [DataMember(Name = "TipoDesc", IsRequired = true)]
        [XmlAttribute("TipoDesc")]
        public String TipoDesc { get; set; }
    }
}