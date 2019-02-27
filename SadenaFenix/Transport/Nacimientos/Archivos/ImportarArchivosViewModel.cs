using SadenaFenix.Models.Usuarios;
using System.Collections.Generic;
using System.Web;

namespace SadenaFenix.Transport.Nacimientos.Archivos
{
    public class ImportarArchivosViewModel
    {
        public HttpPostedFileBase SinacFile { get; set; }

        public HttpPostedFileBase SicFile { get; set; }

        public Usuario Usuario { get; set; }

        public string Message { get; set; }

    }
}
