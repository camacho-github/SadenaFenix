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
    public class Oficina
    {
        [NotMapped]
        [DisplayName("Municipios")]
        public List<Municipio> MunicipioLista { get; set; }

        [NotMapped]
        [DisplayName("Localidades")]
        public List<Localidad> LocalidadLista { get; set;}

        [NotMapped]
        [DisplayName("Oficinas")]
        public List<TipoOficina> OficinasLista { get; set; }

        [DataMember(Name = "OId", IsRequired = true)]
        [XmlAttribute("OId")]
        public int OId { get; set; }

        [DataMember(Name = "OficinaId", IsRequired = true)]
        [XmlAttribute("OficinaId")]
        public int OficinaId { get; set; }

        [DataMember(Name = "TipoId", IsRequired = true)]
        [XmlAttribute("TipoId")]
        public int TipoId { get; set; }

        [DataMember(Name = "TipoInstitucion", IsRequired = true)]
        [XmlAttribute("TipoInstitucion")]
        public string TipoInstitucion { get; set; }

        [DataMember(Name = "Institucion", IsRequired = true)]
        [XmlAttribute("Institucion")]
        public string Institucion { get; set; }

        [DataMember(Name = "Latitud,", IsRequired = true)]
        [XmlAttribute("Latitud,")]
        public string Latitud { get; set; }

        [DataMember(Name = "Longitud,", IsRequired = true)]
        [XmlAttribute("Longitud,")]
        public string Longitud { get; set; }

        [DataMember(Name = "Region", IsRequired = true)]
        [XmlAttribute("Region")]
        public string Region { get; set; }

        [DataMember(Name = "EdoId", IsRequired = true)]
        [XmlAttribute("EdoId")]
        public int EdoId { get; set; }

        [DataMember(Name = "MpioId,", IsRequired = true)]
        [XmlAttribute("MpioId,")]
        public int MpioId { get; set; }

        [DataMember(Name = "MpioDesc,", IsRequired = true)]
        [XmlAttribute("MpioDesc,")]
        public string MpioDesc { get; set; }

        [DataMember(Name = "LocId,", IsRequired = true)]
        [XmlAttribute("LocId,")]
        public int LocId { get; set; }

        [DataMember(Name = "LocDesc", IsRequired = true)]
        [XmlAttribute("LocDesc")]
        public string LocDesc { get; set; }

        [DataMember(Name = "Calle", IsRequired = true)]
        [XmlAttribute("Calle")]
        public string Calle { get; set; }

        [DataMember(Name = "Numero", IsRequired = true)]
        [XmlAttribute("Numero")]
        public string Numero { get; set; }

        [DataMember(Name = "Colonia", IsRequired = true)]
        [XmlAttribute("Colonia")]
        public string Colonia { get; set; }

        [DataMember(Name = "CP", IsRequired = true)]
        [XmlAttribute("CP")]
        public string CP { get; set; }

        [DataMember(Name = "EntreCalles", IsRequired = true)]
        [XmlAttribute("EntreCalles")]
        public string EntreCalles { get; set; }

        [DataMember(Name = "HorarioAtencion", IsRequired = true)]
        [XmlAttribute("HorarioAtencion")]
        public string HorarioAtencion { get; set; }

        [DataMember(Name = "Telefono", IsRequired = true)]
        [XmlAttribute("Telefono")]
        public string Telefono { get; set; }

        [DataMember(Name = "OficialNombre", IsRequired = true)]
        [XmlAttribute("OficialNombre")]
        public string OficialNombre { get; set; }

        [DataMember(Name = "OficialApellidos", IsRequired = true)]
        [XmlAttribute("OficialApellidos")]
        public string OficialApellidos { get; set; }

        [EmailAddress]
        [DataMember(Name = "CorreoE", IsRequired = true)]
        [XmlAttribute("CorreoE")]
        [DisplayName("Correo Electrónico")]
        public String CorreoE { get; set; }

        [DataMember(Name = "InvSerLuz", IsRequired = true)]
        [XmlAttribute("InvSerLuz")]
        public int InvSerLuz { get; set; }

        [DataMember(Name = "InvSerAgua", IsRequired = true)]
        [XmlAttribute("InvSerAgua")]
        public int InvSerAgua { get; set; }

        [DataMember(Name = "InvLocalPropio", IsRequired = true)]
        [XmlAttribute("InvLocalPropio")]
        public int InvLocalPropio { get; set; }

        [DataMember(Name = "InvEscritorios", IsRequired = true)]
        [XmlAttribute("InvEscritorios")]
        public int InvEscritorios { get; set; }

        [DataMember(Name = "InvSerSanitario", IsRequired = true)]
        [XmlAttribute("InvSerSanitario")]
        public int InvSerSanitario { get; set; }              

        [DataMember(Name = "InvSillas", IsRequired = true)]
        [XmlAttribute("InvSillas")]
        public int InvSillas { get; set; }

        [DataMember(Name = "InvArchiveros", IsRequired = true)]
        [XmlAttribute("InvArchiveros")]
        public int InvArchiveros { get; set; }

        [DataMember(Name = "InvCompPriv", IsRequired = true)]
        [XmlAttribute("InvCompPriv")]
        public int InvCompPriv { get; set; }

        [DataMember(Name = "InvCompGob", IsRequired = true)]
        [XmlAttribute("InvCompGob")]
        public int InvCompGob { get; set; }

        [DataMember(Name = "InvEscanPriv", IsRequired = true)]
        [XmlAttribute("InvEscanPriv")]
        public int InvEscanPriv { get; set; }

        [DataMember(Name = "InvEscanGob", IsRequired = true)]
        [XmlAttribute("InvEscanGob")]
        public int InvEscanGob { get; set; }

        [DataMember(Name = "InvImpPriv", IsRequired = true)]
        [XmlAttribute("InvImpPriv")]
        public int InvImpPriv { get; set; }

        [DataMember(Name = "InvImpGob", IsRequired = true)]
        [XmlAttribute("InvImpGob")]
        public int InvImpGob { get; set; }

        [DataMember(Name = "EquiNet", IsRequired = true)]
        [XmlAttribute("EquiNet")]
        public int EquiNet { get; set; }

        [DataMember(Name = "EquiTrabNet", IsRequired = true)]
        [XmlAttribute("EquiTrabNet")]
        public int EquiTrabNet { get; set; }

        [DataMember(Name = "EquiVentExpress", IsRequired = true)]
        [XmlAttribute("EquiVentExpress")]
        public int EquiVentExpress { get; set; }

        [DataMember(Name = "EquiConDrc", IsRequired = true)]
        [XmlAttribute("EquiConDrc")]
        public int EquiConDrc { get; set; }

        [DataMember(Name = "ExpideCurp", IsRequired = true)]
        [XmlAttribute("ExpideCurp")]
        public int ExpideCurp { get; set; }

        [DataMember(Name = "ExpideActasForaneas", IsRequired = true)]
        [XmlAttribute("ExpideActasForaneas")]
        public int ExpideActasForaneas { get; set; }

        [DataMember(Name = "EstatusId", IsRequired = true)]
        [XmlAttribute("EstatusId")]
        public int EstatusId { get; set; }
    }
}