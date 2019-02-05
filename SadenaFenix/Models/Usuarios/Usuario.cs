using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Usuarios
{
    [DataContract]
    public class Usuario
    {
        [DataMember(Name = "SesionId", IsRequired = true)]
        [XmlAttribute("SesionId")]
        public int SesionId { get; set; }

        [DataMember(Name = "UsuarioId", IsRequired = true)]
        [XmlAttribute("UsuarioId")]
        public int UsuarioId { get; set; }

        [DataMember(Name = "UsuarioDesc", IsRequired = true)]
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

        [DataMember(Name = "Rol", IsRequired = true)]
        [XmlAttribute("Rol")]
        public Rol Rol { get; set; }

        [DataMember(Name = "Json", IsRequired = true)]
        [XmlAttribute("Json")]
        public string Json { get; set; }
    }
}
