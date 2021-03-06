﻿using SadenaFenix.Models.Nacimientos.Archivos;
using SadenaFenix.Services;
using SadenaFenix.Transport.Nacimientos.Archivos;
using SadenaFenix.Transport.Usuarios.Acceso;
using System.Collections.ObjectModel;
using System.IO;
using System.Web;

namespace SadenaFenix.Facade.Nacimientos.Archivos
{
    public class ArchivosFacade
    {
        /* Import files */
        public CabeceroRespuesta SalvarArchivos(ImportarArchivosViewModel viewModel)
        {
            CabeceroRespuesta cabeceroRespuesta;
            Servicio servicio = new Servicio();

            /* Processing uploaded files */
            HttpPostedFileBase sinacFileBase = viewModel.SinacFile;
            HttpPostedFileBase sicFileBase = viewModel.SicFile;

            /* Validation .*/
            //if (sinacFileBase.ContentLength <= 0 || sicFileBase.ContentLength <= 0)
            //{
            //    cabeceroRespuesta = new CabeceroRespuesta();
            //    cabeceroRespuesta.CodigoRespuesta = -1;
            //    cabeceroRespuesta.MensajeRespuesta = "Los archivos son incorrectos o no tienen datos.";
            //    return cabeceroRespuesta;
            //}

            /* Temp location */
            var tempPath = Path.GetTempPath();

            /* Preparing request to service */
            CabeceroPeticion cabeceroPeticion = new CabeceroPeticion
            {
                SesionId = viewModel.Usuario.SesionId
            };
            PreCargaPeticion preCargaPeticion = new PreCargaPeticion()
            {
                Cabecero = cabeceroPeticion,
                ColArchivo = new Collection<Archivo>()
            };

            /* SINAC */
            if (sinacFileBase != null && sinacFileBase.ContentLength > 0)
            {
                var pathSINAC = Path.Combine(tempPath, Path.GetFileName(sinacFileBase.FileName));
                sinacFileBase.SaveAs(pathSINAC);

                var index_anio = sinacFileBase.FileName.IndexOf(".SINAC_");
                var anio = sinacFileBase.FileName.Substring(index_anio + 7, 4);
                Archivo archivoSINAC = new Archivo
                {
                    Extension = sinacFileBase.ContentType,
                    Nombre = pathSINAC,
                    Ano = anio
                };
                archivoSINAC.IdentificarTablaSINAC();

                preCargaPeticion.ColArchivo.Add(archivoSINAC);
            }


            /* SIC */
            if (sicFileBase != null && sicFileBase.ContentLength > 0)
            {
                var pathSIC = Path.Combine(tempPath, Path.GetFileName(sicFileBase.FileName));
                var index_anio = sicFileBase.FileName.IndexOf(".SIC_");
                var anio = sicFileBase.FileName.Substring(index_anio + 5, 4);
                sicFileBase.SaveAs(pathSIC);
                Archivo archivoSIC = new Archivo
                {
                    Extension = sicFileBase.ContentType,
                    Nombre = pathSIC,
                    Ano = anio
                };
                archivoSIC.IdentificarTablaSIC();

                preCargaPeticion.ColArchivo.Add(archivoSIC);
            }

            /* Almacenar archivos */
            cabeceroRespuesta = servicio.PreCargarDatos(preCargaPeticion);
            if (cabeceroRespuesta.EsRespuestaExistosa())
            {
                cabeceroRespuesta.MensajeRespuesta = "Archivos guardados exitosamente.";
            }
            else
            {
                return cabeceroRespuesta;
            }            

            /* Procesar archivos. */
            cabeceroRespuesta = servicio.ProcesarCarga(preCargaPeticion);
            if (cabeceroRespuesta.EsRespuestaExistosa())
            {
                cabeceroRespuesta.MensajeRespuesta = "Los archivos han sido guardados y procesados exitosamente, los datos pueden ser consultados ahora.";
            }
            else
            {
                return cabeceroRespuesta;
            }
            return cabeceroRespuesta;
        }

    }

}
