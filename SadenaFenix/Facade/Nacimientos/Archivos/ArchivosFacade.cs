using SadenaFenix.Models.Nacimientos.Archivos;
using SadenaFenix.Services;
using SadenaFenix.Transport.Nacimientos.Archivos;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.IO;
using System.Web;

namespace SadenaFenix.Facade.Nacimientos.Archivos
{
    public class ArchivosFacade
    {
        /* Import files */
        public CabeceroRespuesta SalvarArchivos(ImportarArchivosViewModel viewModel)
        {
            CabeceroRespuesta cabeceroRespuesta = new CabeceroRespuesta();
            Servicio servicio = new Servicio();

            /* Processing uploaded files */
            HttpPostedFileBase sinacFileBase = viewModel.SinacFile;
            HttpPostedFileBase sicFileBase = viewModel.SicFile;

            /* Validation .*/
            if (sinacFileBase.ContentLength <= 0 || sicFileBase.ContentLength <= 0)
            {
                cabeceroRespuesta.CodigoRespuesta = -1;
                cabeceroRespuesta.MensajeRespuesta = "Archivos incorrectos o sin datos.";
                return cabeceroRespuesta;
            }

            /* Temp location */
            var tempPath = Path.GetTempPath();

            /* SINAC */
            var pathSINAC = Path.Combine(tempPath, Path.GetFileName(sinacFileBase.FileName));
            sinacFileBase.SaveAs(pathSINAC);
            Archivo archivoSINAC = new Archivo
            {
                Extension = sinacFileBase.ContentType,
                Nombre = pathSINAC,
                Ano = "2019"
            };

            /* SIC */
            var pathSIC = Path.Combine(tempPath, Path.GetFileName(sicFileBase.FileName));
            sicFileBase.SaveAs(pathSIC);
            Archivo archivoSIC = new Archivo
            {
                Extension = sicFileBase.ContentType,
                Nombre = pathSIC,
                Ano = "2019"
            };
            CabeceroPeticion cabeceroPeticion = new CabeceroPeticion
            {
                SesionId = viewModel.Usuario.SesionId
            };
            PreCargaPeticion preCargaPeticion = new PreCargaPeticion();

            /*"Archivos guardados exitosamente.";*/
            return cabeceroRespuesta;
        }

    }

}
