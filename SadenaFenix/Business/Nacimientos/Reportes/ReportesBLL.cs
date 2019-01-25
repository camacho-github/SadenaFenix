using SadenaFenix.Daos.Nacimientos.Reportes;
using SadenaFenix.Commons.Utilerias;
using SadenaFenix.Excepcions;
using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Transport.Nacimientos.Reportes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SadenaFenix.Business.Nacimientos.Reportes
{
    public class ReportesBLL
    {
        #region Variables de Instancia
        private ReporteDAO reporteDAO;
        private const int ARCHIVO_LOCALIDAD = 1;
        private const int ARCHIVO_SINAC = 2;
        private const int ARCHIVO_SIC = 3;
        #endregion

        #region Constructor
        public ReportesBLL()
        {
            reporteDAO = new ReporteDAO();
        }
        #endregion

        #region Métodos reportes
        public SubregistroNacimientosRespuesta ConsultaSubregistroNacimientos(Collection<string> colAnos, Collection<string> colMeses, Collection<Municipio> colMunicipios)
        {
            try
            {
                IList<string> anosLista = new List<string>(colAnos);
                string anosUnion = string.Join(",", anosLista);

                IList<string> mesesLista = new List<string>(colMeses);
                string mesesUnion = string.Join(",", mesesLista);

                IList<string> municipiosLista = new List<string>();
                foreach (Municipio m in colMunicipios)
                {
                    municipiosLista.Add(m.MpioId.ToString());
                }
                string municipiosUnion = string.Join(",", municipiosLista);

                return reporteDAO.ConsultaSubregistroNacimientos(anosUnion, mesesUnion, municipiosUnion);
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
                    throw new BusinessException("No se completó la consulta de subregistro, favor de intentar nuevamente: " + e.Message);
                }

            }
        }
        #endregion
    }
}
