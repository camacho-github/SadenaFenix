using SadenaFenix.Transport.Usuarios.Acceso;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Catalogos
{
    [DataContract]
    public class CatalogosSocioEconomicaPeticion
    {
        [DataMember(Name = "cabRequest", IsRequired = true)]
        [XmlAttribute("cabRequest")]
        public CabeceroPeticion HeaderRequest { get; set; }

    }
}