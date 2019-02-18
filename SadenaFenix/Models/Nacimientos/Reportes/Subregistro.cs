using System;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Nacimientos.Reportes
{
    [DataContract]
    public class Subregistro
    {
        [DataMember(Name = "Folio", IsRequired = true)]
        [XmlAttribute("Folio")]
        public String Folio { get; set; }

        [DataMember(Name = "FechaNacimiento", IsRequired = true)]
        [XmlAttribute("FechaNacimiento")]
        public String FechaNacimiento { get; set; }

        [DataMember(Name = "HoraNacimiento", IsRequired = true)]
        [XmlAttribute("HoraNacimiento")]
        public String HoraNacimiento { get; set; }

        [DataMember(Name = "SexoId", IsRequired = true)]
        [XmlAttribute("SexoId")]
        public int SexoId { get; set; }

        [DataMember(Name = "SexoDesc", IsRequired = true)]
        [XmlAttribute("SexoDesc")]
        public String SexoDesc { get; set; }

        [DataMember(Name = "EdoId", IsRequired = true)]
        [XmlAttribute("EdoId")]
        public int EdoId { get; set; }

        [DataMember(Name = "EdoDesc", IsRequired = true)]
        [XmlAttribute("EdoDesc")]
        public String EdoDesc { get; set; }

        [DataMember(Name = "MpioId", IsRequired = true)]
        [XmlAttribute("MpioId")]
        public int MpioId { get; set; }

        [DataMember(Name = "MpioDesc", IsRequired = true)]
        [XmlAttribute("MpioDesc")]
        public String MpioDesc { get; set; }

        [DataMember(Name = "LocId", IsRequired = true)]
        [XmlAttribute("LocId")]
        public int LocId { get; set; }

        [DataMember(Name = "LocDesc", IsRequired = true)]
        [XmlAttribute("LocDesc")]
        public String LocDesc { get; set; }

        [DataMember(Name = "Calle", IsRequired = true)]
        [XmlAttribute("Calle")]
        public String Calle { get; set; }

        [DataMember(Name = "NoExt", IsRequired = true)]
        [XmlAttribute("NoExt")]
        public String NoExt { get; set; }

        [DataMember(Name = "NoInt", IsRequired = true)]
        [XmlAttribute("NoInt")]
        public String NoInt { get; set; }

        [DataMember(Name = "Edad", IsRequired = true)]
        [XmlAttribute("Edad")]
        public int Edad { get; set; }

        [DataMember(Name = "NumNacimiento", IsRequired = true)]
        [XmlAttribute("NumNacimiento")]
        public int NumNacimiento { get; set; }

        [DataMember(Name = "Ocupacion", IsRequired = true)]
        [XmlAttribute("Ocupacion")]
        public String Ocupacion { get; set; }

        [DataMember(Name = "EdoCivilId", IsRequired = true)]
        [XmlAttribute("EdoCivilId")]
        public int EdoCivilId { get; set; }

        [DataMember(Name = "EdoCivilDesc", IsRequired = true)]
        [XmlAttribute("EdoCivilDesc")]
        public String EdoCivilDesc { get; set; }

        [DataMember(Name = "EscolId", IsRequired = true)]
        [XmlAttribute("EscolId")]
        public int EscolId { get; set; }

        [DataMember(Name = "EscolDesc", IsRequired = true)]
        [XmlAttribute("EscolDesc")]
        public String EscolDesc { get; set; }

    }
}
