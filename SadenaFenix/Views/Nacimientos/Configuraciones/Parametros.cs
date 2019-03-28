using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace SadenaFenix.Views.Nacimientos.Configuraciones
{
    public class Parametros
    {
        [DataMember(Name = "NoDiasExtemporaneos", IsRequired = true)]
        [XmlAttribute("NoDiasExtemporaneos")]
        [DisplayName("Número de días Extemporaneos")]
        public int NoDiasExtemporaneos { get; set; }
               
    }
}