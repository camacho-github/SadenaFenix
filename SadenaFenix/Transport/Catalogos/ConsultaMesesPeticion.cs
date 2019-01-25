using SadenaFenix.Transport.Usuarios.Acceso;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Catalogos
{
    [DataContract]
    public class ConsultaMesesPeticion
    {
        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroPeticion Cabecero { get; set; }

        [DataMember(Name = "Anio", IsRequired = true)]
        [XmlAttribute("Anio")]
        public string Anio { get; set; }
    }
}