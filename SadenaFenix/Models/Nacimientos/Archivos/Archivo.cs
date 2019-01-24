using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Models.Nacimientos.Archivos
{
    [DataContract]
    public class Archivo
    {
        [DataMember(Name = "Identificador", IsRequired = true)]
        [XmlAttribute("Identificador")]
        public int Identificador { get; set; }

        [DataMember(Name = "Nombre", IsRequired = true)]
        [XmlAttribute("Nombre")]
        public string Nombre { get; set; }

        [DataMember(Name = "Extension", IsRequired = true)]
        [XmlAttribute("Extension")]
        public string Extension { get; set; }

        [DataMember(Name = "Ano", IsRequired = true)]
        [XmlAttribute("Ano")]
        public string Ano { get; set; }

        public void IdentificarCatalogoLocalidad()
        {
            Identificador = Constantes.Constantes.IDENTIFICADOR_LOCALIDAD;
        }

        public void IdentificarTablaSINAC()
        {
            Identificador = Constantes.Constantes.IDENTIFICADOR_SINAC;
        }

        public void IdentificarTablaSIC()
        {
            Identificador = Constantes.Constantes.IDENTIFICADOR_SIC;
        }

    }
}
