using SadenaFenix.Models.Catalogos.Geografia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DisplayName("Oficialia ID")]
        public int OficialiaId { get; set; }

        [DataMember(Name = "MpioId", IsRequired = true)]
        [XmlAttribute("MpioId")]
        [DisplayName("Municipio ID")]
        public int MpioId { get; set; }

        [DataMember(Name = "MpioDesc", IsRequired = true)]
        [XmlAttribute("MpioDesc")]
        [DisplayName("Municipio")]
        public string MpioDesc { get; set; }

        [NotMapped]
        [DisplayName("Municipios")]
        public List<Municipio> MunicipioLista { get; set; }

        [DataMember(Name = "LocId", IsRequired = true)]
        [XmlAttribute("LocId")]
        [DisplayName("Localidad ID")]
        public int LocId { get; set; }

        [DataMember(Name = "LocDesc", IsRequired = true)]
        [XmlAttribute("LocDesc")]
        [DisplayName("Localidad")]
        public string LocDesc { get; set; }

        [NotMapped]
        [DisplayName("Localidades")]
        public List<Localidad> LocalidadLista { get; set;}

        [DataMember(Name = "Calle", IsRequired = true)]
        [XmlAttribute("Calle")]
        public String Calle { get; set; }

        [DataMember(Name = "Numero", IsRequired = true)]
        [XmlAttribute("Numero")]
        [DisplayName("Número")]
        public String Numero { get; set; }

        [DataMember(Name = "Colonia", IsRequired = true)]
        [XmlAttribute("Colonia")]
        public String Colonia { get; set; }

        [DataMember(Name = "CP", IsRequired = true)]
        [XmlAttribute("CP")]
        [DisplayName("C.P.")]
        public String CP { get; set; }

        [DataMember(Name = "Telefono", IsRequired = true)]
        [XmlAttribute("Telefono")]
        [DisplayName("Teléfono")]
        public String Telefono { get; set; }

        [DataMember(Name = "Nombres", IsRequired = true)]
        [XmlAttribute("Nombres")]
        public String Nombres { get; set; }

        [DataMember(Name = "Apellidos", IsRequired = true)]
        [XmlAttribute("Apellidos")]
        public String Apellidos { get; set; }

        [EmailAddress]
        [DataMember(Name = "CorreoE", IsRequired = true)]
        [XmlAttribute("CorreoE")]
        [DisplayName("Correo Electrónico")]
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