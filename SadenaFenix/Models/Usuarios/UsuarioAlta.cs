using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Usuarios
{
    [DataContract]
    public class UsuarioAlta
    {
        [DataMember(Name = "UsuarioId")]
        [XmlAttribute("UsuarioId")]
        public int UsuarioId { get; set; }

        [DataMember(Name = "UsuarioDesc", IsRequired = true)]
        [DisplayName("Usuario")]
        [XmlAttribute("UsuarioDesc")]
        public string UsuarioDesc { get; set; }

        [DisplayName("Correo")]
        [DataMember(Name = "CorreoE", IsRequired = true)]
        [XmlAttribute("CorreoE")]
        public string CorreoE { get; set; }

        [DisplayName("Contraseña")]
        [XmlAttribute("Contrasenia")]
        [DataMember(Name = "Contrasenia", IsRequired = true)]
        public string Contrasenia { get; set; }
        
        [DataMember(Name = "RolId", IsRequired = true)]
        [XmlAttribute("RolId")]
        public int RolId { get; set; }

        [NotMapped]
        [DisplayName("Rol")]        
        public string RolDesc { get; set; }

        [NotMapped]
        [DisplayName("Roles")]
        public List<Rol> RolesLista { get; set; }

        [DataMember(Name = "StatusId")]
        [XmlAttribute("StatusId")]
        public int StatusId { get; set; }

    }
}
