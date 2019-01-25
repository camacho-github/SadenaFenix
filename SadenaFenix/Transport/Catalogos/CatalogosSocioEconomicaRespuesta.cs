using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Models.Catalogos.Socieconomica;
using SadenaFenix.Models.Catalogos.Socioeconomica;
using SadenaFenix.Transport.Usuarios.Acceso;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Catalogos
{
    [DataContract]
    public class CatalogosSocioEconomicaRespuesta
    {
        public CatalogosSocioEconomicaRespuesta()
        {
            Cabecero = new CabeceroRespuesta();
        }

        [DataMember(Name = "Cabecero", IsRequired = true)]
        [XmlAttribute("Cabecero")]
        public CabeceroRespuesta Cabecero { get; set; }

        [DataMember(Name = "colEstatusRegistro", IsRequired = true)]
        [XmlAttribute("colEstatusRegistro")]
        public Collection<EstatusRegistro> ColEstatusRegistro { get; set; }

        [DataMember(Name = "cabResponse", IsRequired = true)]
        [XmlAttribute("cabResponse")]
        public CabeceroRespuesta EncabezadoResponse { get; set; }

        [DataMember(Name = "colSexo", IsRequired = true)]
        [XmlAttribute("colSexo")]
        public Collection<Sexo> ColSexo { get; set; }

        [DataMember(Name = "colEdoCivil", IsRequired = true)]
        [XmlAttribute("colSexo")]
        public Collection<EdoCivil> ColEdoCivil { get; set; }

        [DataMember(Name = "colEscolaridad", IsRequired = true)]
        [XmlAttribute("colEscolaridad")]
        public Collection<Escolaridad> ColEscolaridad { get; set; }
                
    }
}