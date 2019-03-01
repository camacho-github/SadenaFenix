using SadenaFenix.Models.Nacimientos.Archivos;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Nacimientos.Archivos
{
    [DataContract]
    public class PreCargaPeticion
    {
        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroPeticion Cabecero { get; set; }

        [DataMember(Name = "ColArchivo", IsRequired = true)]
        [XmlAttribute("ColArchivo")]
        public Collection<Archivo> ColArchivo { get; set; }
    }
}
