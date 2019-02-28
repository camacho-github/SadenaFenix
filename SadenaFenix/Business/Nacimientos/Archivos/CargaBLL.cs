using SadenaFenix.Daos.Nacimientos.Archivos;
using SadenaFenix.Commons.Utilerias;
using SadenaFenix.Excepcions;
using SadenaFenix.Models.Constantes;
using SadenaFenix.Models.Nacimientos.Archivos;
using SadenaFenix.Transport.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SadenaFenix.Business.Nacimientos.Archivos
{
    public class CargaBLL
    {
        #region Variables de Instancia
        private CargaDAO cargaDAO;
        private const int ARCHIVO_LOCALIDAD = 1;
        private const int ARCHIVO_SINAC = 2;
        private const int ARCHIVO_SIC = 3;
        #endregion

        #region Constructor
        public CargaBLL()
        {
            cargaDAO = new CargaDAO();
        }
        #endregion

        #region Métodos Públicos

        public bool PreCargarDatos(int sesionId, List<Archivo> archivos)
        {

            try
            {
                foreach (Archivo archivo in archivos)
                {

                    switch (archivo.Identificador)
                    {
                        case Constantes.IDENTIFICADOR_LOCALIDAD:
                            cargaDAO.PreCargarArchivoLocalidad(ObtieneNombreArchivo(archivo));
                            break;
                        case Constantes.IDENTIFICADOR_SINAC:
                            cargaDAO.PreCargarArchivoSINAC(ObtieneNombreArchivo(archivo));
                            break;
                        case Constantes.IDENTIFICADOR_SIC:
                            cargaDAO.PreCargarArchivoSIC(ObtieneNombreArchivo(archivo));
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }

                    cargaDAO.InsertarBitacoraCarga(sesionId, archivo.Identificador, archivo.Ano, archivo.Nombre, archivo.Extension);
                }
            }

            catch (Exception e)
            {
                Bitacora.Error(e.Message);
                throw new BusinessException("La carga no fue exitosa, favor de validar los archivos a importar: " + e.Message);
            }

            return true;
        }

        public void ProcesarCarga()
        {
            try
            {
                cargaDAO.ProcesarCargaSINAC();
                cargaDAO.ProcesarCargaSIC();
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);
                throw new BusinessException("El procesamiento de la carga no fue completado exitosamente, favor de intentar nuevamente: " + e.Message);
            }

        }

        public CatalogosCargasRespuesta ObtenerCatalogosCargas()
        {

            try
            {
                return cargaDAO.ObtenerCatalogosCargas();
            }
            catch (DAOException e)
            {
                Bitacora.Error(e.Message);
                if (e.Codigo == 1)
                {
                    throw new BusinessException(e.Message);
                }
                else
                {
                    throw new BusinessException("No se completó la consulta de catálogos, favor de intentar nuevamente: " + e.Message);
                }

            }
        }


        #endregion


        #region Métodos Privados
        private static string ObtieneNombreArchivo(Archivo archivo)
        {
            return archivo.Nombre; // + "." + archivo.Extension;
        }
        #endregion
    }
}
