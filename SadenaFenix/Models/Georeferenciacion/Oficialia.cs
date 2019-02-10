using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Georeferenciacion
{
    public class Oficialia
    {
        [DataMember(Name = "OId", IsRequired = true)]
        [XmlAttribute("OId")]
        public int OId { get; set; }

        [DataMember(Name = "OficialiaId", IsRequired = true)]
        [XmlAttribute("OficialiaId")]
        public int OficialiaId { get; set; }

        [DataMember(Name = "EdoId", IsRequired = true)]
        [XmlAttribute("EdoId")]
        public int EdoId { get; set; }

        [DataMember(Name = "MpioId", IsRequired = true)]
        [XmlAttribute("MpioId")]
        public int MpioId { get; set; }

        [DataMember(Name = "LocId", IsRequired = true)]
        [XmlAttribute("LocId")]
        public int LocId { get; set; }
        
        [DataMember(Name = "Calle", IsRequired = true)]
        [XmlAttribute("Calle")]
        public String Calle { get; set; }

        [DataMember(Name = "Numero", IsRequired = true)]
        [XmlAttribute("Numero")]
        public String Numero { get; set; }

        [DataMember(Name = "Colonia", IsRequired = true)]
        [XmlAttribute("Colonia")]
        public String Colonia { get; set; }

        [DataMember(Name = "CP", IsRequired = true)]
        [XmlAttribute("CP")]
        public String CP { get; set; }

        [DataMember(Name = "Telefono", IsRequired = true)]
        [XmlAttribute("Telefono")]
        public String Telefono { get; set; }

        [DataMember(Name = "Nombres", IsRequired = true)]
        [XmlAttribute("Nombres")]
        public String Nombres { get; set; }

        [DataMember(Name = "Apellidos", IsRequired = true)]
        [XmlAttribute("Apellidos")]
        public String Apellidos { get; set; }

        [DataMember(Name = "CorreoE", IsRequired = true)]
        [XmlAttribute("CorreoE")]
        public String CorreoE { get; set; }

        [DataMember(Name = "Latitud", IsRequired = true)]
        [XmlAttribute("Latitud")]
        public String Latitud { get; set; }

        [DataMember(Name = "Longitud", IsRequired = true)]
        [XmlAttribute("Longitud")]
        public String Longitud { get; set; }

        [DataMember(Name = "Observaciones", IsRequired = true)]
        [XmlAttribute("Observaciones")]
        public String Observaciones { get; set; }

        [DataMember(Name = "EstatusId", IsRequired = true)]
        [XmlAttribute("EstatusId")]
        public String EstatusId { get; set; }
    }
}