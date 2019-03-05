using SadenaFenix.Models.Nacimientos.Archivos;
using SadenaFenix.Models.Usuarios;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.Generic;
using System.Web;

namespace SadenaFenix.Transport.Nacimientos.Archivos
{
    public class ImportarArchivosViewModel
    {
        public ImportarArchivosViewModel()
        {
            SinacArchivo = new Archivo();
            SicArchivo = new Archivo();
        }

        public HttpPostedFileBase SinacFile { get; set; }

        public HttpPostedFileBase SicFile { get; set; }

        public Usuario Usuario { get; set; }

        public string UserJson { get; set; }

        public Archivo SinacArchivo { get; set; }

        public Archivo SicArchivo { get; set; }

        public CabeceroRespuesta CabeceroRespuesta { get; set; }

    }
}
