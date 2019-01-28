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
using SadenaFenix.Models.Nacimientos.Consultas;
using SadenaFenix.Models.Constantes;

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
        public TotalesSubregistroNacimientosRespuesta ConsultaTotalesSubregistroNacimientos(Collection<string> colAnos, Collection<string> colMeses, Collection<Municipio> colMunicipios)
        {
            TotalesSubregistroNacimientosRespuesta totalesSubregistroNacimientosRespuesta = new TotalesSubregistroNacimientosRespuesta();

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

                totalesSubregistroNacimientosRespuesta = reporteDAO.ConsultaTotalesSubregistroNacimientos(anosUnion, mesesUnion, municipiosUnion);

                int total = 0;
                foreach (SubregistroTotal s in totalesSubregistroNacimientosRespuesta.ColTotales)
                {
                    if (s.IdGrupo < Constantes.REGISTRO_DUPLICADO)
                    {
                        total += s.Total;
                    }
                }

                totalesSubregistroNacimientosRespuesta.Total = total;

                foreach (SubregistroTotal s in totalesSubregistroNacimientosRespuesta.ColTotales)
                {
                    if (s.IdGrupo < Constantes.REGISTRO_DUPLICADO)
                    {
                        decimal d = (decimal)s.Total * 100 / total;
                        s.TotalPorcentaje = Math.Round(d, 2);

                        int caseSwitch = s.IdGrupo;

                        switch (caseSwitch)
                        {
                            case Constantes.SUBREGISTRO:
                                totalesSubregistroNacimientosRespuesta.TotalSubregistro = s.Total;
                                totalesSubregistroNacimientosRespuesta.PorcentajeSubregistro = s.TotalPorcentaje;
                                break;
                            case Constantes.REGISTRO_OPORTUNO:
                                totalesSubregistroNacimientosRespuesta.TotalRegistroOportuno = s.Total;
                                totalesSubregistroNacimientosRespuesta.PorcentajeRegistroOportuno = s.TotalPorcentaje;
                                break;
                            case Constantes.REGISTRO_EXTEMPORANEO:
                                totalesSubregistroNacimientosRespuesta.TotalRegistroExtemporaneo = s.Total;
                                totalesSubregistroNacimientosRespuesta.PorcentajeRegistroExtemporaneo = s.TotalPorcentaje;
                                break;
                            default:
                                Console.WriteLine("Default case");
                                break;
                        }
                    }
                }
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

            return totalesSubregistroNacimientosRespuesta;
        }

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
