using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SadenaFenix.Transport.Usuarios.Acceso
{
    [DataContract]
    public class CabeceroRespuesta
    {
        public CabeceroRespuesta(int codigoRespuesta, string mensajeRespuesta)
        {
            this.CodigoRespuesta = codigoRespuesta;
            this.MensajeRespuesta = mensajeRespuesta;
        }
        public CabeceroRespuesta() { }
        [DataMember(Name = "CodigoRespuesta", IsRequired = true)]
        [XmlAttribute("CodigoRespuesta")]
        public int CodigoRespuesta { get; set; }

        [DataMember(Name = "MensajeRespuesta", IsRequired = true)]
        [XmlAttribute("MensajeRespuesta")]
        public string MensajeRespuesta { get; set; }

        public bool EsRespuestaExistosa()
        {
            return this.CodigoRespuesta == 0;
        }
    }
}
