using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Transport.Usuarios.Acceso;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Catalogos
{
    [DataContract]
    public class CatalogoLocalidadRespuesta
    {
        public CatalogoLocalidadRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }

        [DataMember(Name = "colLocalidad", IsRequired = true)]
        [XmlAttribute("colLocalidad")]
        public Collection<Localidad> ColLocalidad { get; set; }

    }
}