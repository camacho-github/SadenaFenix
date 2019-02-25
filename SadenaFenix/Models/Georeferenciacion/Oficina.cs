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
        public List<Localidad> LocalidadLista { get; set; }

        [NotMapped]
        [DisplayName("Tipo Oficina")]
        public List<TipoOficina> TipoLista { get; set; }

        [DataMember(Name = "OId", IsRequired = true)]
        [XmlAttribute("OId")]
        public int OId { get; set; }

        [DataMember(Name = "OficinaId", IsRequired = true)]
        [XmlAttribute("OficinaId")]
        [DisplayName("Oficina ID")]
        public int OficinaId { get; set; }

        [DataMember(Name = "TipoId", IsRequired = true)]
        [XmlAttribute("TipoId")]
        [DisplayName("Tipo ID")]
        public int TipoId { get; set; }

        [DataMember(Name = "TipoDesc", IsRequired = true)]
        [XmlAttribute("TipoDesc")]
        [DisplayName("Tipo de Oficina")]
        public string TipoDesc { get; set; }

        [DataMember(Name = "TipoInstitucion", IsRequired = true)]
        [XmlAttribute("TipoInstitucion")]
        [DisplayName("Tipo de Institución")]
        public string TipoInstitucion { get; set; }

        [DataMember(Name = "Institucion", IsRequired = true)]
        [XmlAttribute("Institucion")]
        [DisplayName("Institución")]
        public string Institucion { get; set; }

        [DataMember(Name = "Latitud,", IsRequired = true)]
        [XmlAttribute("Latitud,")]
        public string Latitud { get; set; }

        [DataMember(Name = "Longitud,", IsRequired = true)]
        [XmlAttribute("Longitud,")]
        public string Longitud { get; set; }

        [DataMember(Name = "Region", IsRequired = true)]
        [XmlAttribute("Region")]
        [DisplayName("Región")]
        public string Region { get; set; }

        [DataMember(Name = "EdoId", IsRequired = true)]
        [XmlAttribute("EdoId")]
        public int EdoId { get; set; }

        [DataMember(Name = "MpioId,", IsRequired = true)]
        [XmlAttribute("MpioId")]
        public int MpioId { get; set; }

        [DataMember(Name = "MpioDesc,", IsRequired = true)]
        [XmlAttribute("MpioDesc")]
        [DisplayName("Municipio")]
        public string MpioDesc { get; set; }

        [DataMember(Name = "LocId,", IsRequired = true)]
        [XmlAttribute("LocId")]
        public int LocId { get; set; }

        [DataMember(Name = "LocDesc", IsRequired = true)]
        [XmlAttribute("LocDesc")]
        [DisplayName("Localidad")]
        public string LocDesc { get; set; }

        [DataMember(Name = "Calle", IsRequired = true)]
        [XmlAttribute("Calle")]
        public string Calle { get; set; }

        [DataMember(Name = "Numero", IsRequired = true)]
        [XmlAttribute("Numero")]
        [DisplayName("Número")]
        public string Numero { get; set; }

        [DataMember(Name = "Colonia", IsRequired = true)]
        [XmlAttribute("Colonia")]
        public string Colonia { get; set; }

        [DataMember(Name = "CP", IsRequired = true)]
        [XmlAttribute("CP")]
        [DisplayName("C.P.")]
        public string CP { get; set; }

        [DataMember(Name = "EntreCalles", IsRequired = true)]
        [XmlAttribute("EntreCalles")]
        [DisplayName("Entre Calles")]
        public string EntreCalles { get; set; }

        [DataMember(Name = "HorarioAtencion", IsRequired = true)]
        [XmlAttribute("HorarioAtencion")]
        [DisplayName("Horario de Atencíon")]
        public string HorarioAtencion { get; set; }

        [DataMember(Name = "Telefono", IsRequired = true)]
        [XmlAttribute("Telefono")]
        [DisplayName("Teléfono")]
        public string Telefono { get; set; }

        [DataMember(Name = "OficialNombre", IsRequired = true)]
        [XmlAttribute("OficialNombre")]
        [DisplayName("Nombre(s)")]
        public string OficialNombre { get; set; }

        [DataMember(Name = "OficialApellidos", IsRequired = true)]
        [XmlAttribute("OficialApellidos")]
        [DisplayName("Apellido(s)")]
        public string OficialApellidos { get; set; }

        [EmailAddress]
        [DataMember(Name = "CorreoE", IsRequired = true)]
        [XmlAttribute("CorreoE")]
        [DisplayName("Correo Electrónico")]
        public String CorreoE { get; set; }

        [DataMember(Name = "InvSerLuz", IsRequired = true)]
        [XmlAttribute("InvSerLuz")]
        [DisplayName("Servicio de luz")]
        public int InvSerLuz { get; set; }

        [DataMember(Name = "InvSerAgua", IsRequired = true)]
        [XmlAttribute("InvSerAgua")]
        [DisplayName("Servicio de agua")]
        public int InvSerAgua { get; set; }

        [DataMember(Name = "InvLocalPropio", IsRequired = true)]
        [XmlAttribute("InvLocalPropio")]
        [DisplayName("Local propio")]
        public int InvLocalPropio { get; set; }

        [DataMember(Name = "InvEscritorios", IsRequired = true)]
        [XmlAttribute("InvEscritorios")]
        [DisplayName("Escritorios")]
        public int InvEscritorios { get; set; }

        [DataMember(Name = "InvSerSanitario", IsRequired = true)]
        [XmlAttribute("InvSerSanitario")]
        [DisplayName("Servicio sanitario")]
        public int InvSerSanitario { get; set; }              

        [DataMember(Name = "InvSillas", IsRequired = true)]
        [XmlAttribute("InvSillas")]
        [DisplayName("Sillas")]
        public int InvSillas { get; set; }

        [DataMember(Name = "InvArchiveros", IsRequired = true)]
        [XmlAttribute("InvArchiveros")]
        [DisplayName("Archiveros/Estantes")]
        public int InvArchiveros { get; set; }

        [DataMember(Name = "InvCompPriv", IsRequired = true)]
        [XmlAttribute("InvCompPriv")]
        [DisplayName("Equipo de cómputo privado")]
        public int InvCompPriv { get; set; }

        [DataMember(Name = "InvCompGob", IsRequired = true)]
        [XmlAttribute("InvCompGob")]
        [DisplayName("Equipo de cómputo de gobierno")]
        public int InvCompGob { get; set; }

        [DataMember(Name = "InvEscanPriv", IsRequired = true)]
        [XmlAttribute("InvEscanPriv")]
        [DisplayName("Escaner privado")]
        public int InvEscanPriv { get; set; }

        [DataMember(Name = "InvEscanGob", IsRequired = true)]
        [XmlAttribute("InvEscanGob")]
        [DisplayName("Escaner de gobierno")]
        public int InvEscanGob { get; set; }

        [DataMember(Name = "InvImpPriv", IsRequired = true)]
        [XmlAttribute("InvImpPriv")]
        [DisplayName("Impresora privada")]
        public int InvImpPriv { get; set; }

        [DataMember(Name = "InvImpGob", IsRequired = true)]
        [XmlAttribute("InvImpGob")]
        [DisplayName("Impresora de gobierno")]
        public int InvImpGob { get; set; }

        [DataMember(Name = "EquiNet", IsRequired = true)]
        [XmlAttribute("EquiNet")]
        [DisplayName("Internet")]
        public int EquiNet { get; set; }

        [DataMember(Name = "EquiTrabNet", IsRequired = true)]
        [XmlAttribute("EquiTrabNet")]
        [DisplayName("Trabajo con internet")]
        public int EquiTrabNet { get; set; }

        [DataMember(Name = "EquiVentExpress", IsRequired = true)]
        [XmlAttribute("EquiVentExpress")]
        [DisplayName("Ventanilla express")]
        public int EquiVentExpress { get; set; }

        [DataMember(Name = "EquiConDrc", IsRequired = true)]
        [XmlAttribute("EquiConDrc")]
        [DisplayName("Conexión con DRC")]
        public int EquiConDrc { get; set; }

        [DataMember(Name = "ExpideCurp", IsRequired = true)]
        [XmlAttribute("ExpideCurp")]
        [DisplayName("Expide CURP")]
        public int ExpideCurp { get; set; }

        [DataMember(Name = "ExpideActasForaneas", IsRequired = true)]
        [XmlAttribute("ExpideActasForaneas")]
        [DisplayName("Expide actas de otros estados")]
        public int ExpideActasForaneas { get; set; }

        [DataMember(Name = "EstatusId", IsRequired = true)]
        [XmlAttribute("EstatusId")]
        public int EstatusId { get; set; }
    }
}