using SadenaFenix.Models.Nacimientos.Reportes;
using SadenaFenix.Transport.Usuarios.Acceso;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Nacimientos.Reportes
{
    [DataContract]
    public class AnalisisSICRespuesta
    {
        public AnalisisSICRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }
        
        [DataMember(Name = "DTs", IsRequired = true)]
        [XmlAttribute("DTs")]
        public Collection<DataTable> DTs { get; set; }     

    }
}