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
using System.Xml;
using Newtonsoft.Json;
using SadenaFenix.Models.Nacimientos.Reportes;

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

        public ReporteSubregistroRespuesta ConsultarReporteTotalesSubregistro(Collection<string> colAnos, Collection<string> colMeses, Collection<Municipio> colMunicipios)
        {
            ReporteSubregistroRespuesta reporte = new ReporteSubregistroRespuesta();

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

                XmlDocument xmlReporte = reporteDAO.ConsultarReporteTotalesSubregistro(anosUnion, mesesUnion, municipiosUnion);
                reporte.XmlReporte = JsonConvert.SerializeXmlNode(xmlReporte);

                Collection<string> cabeceros = new Collection<string>();
                reporte.ColFilas = ObtenerFilas(cabeceros, xmlReporte);
                reporte.ColCabeceros = cabeceros;
                return reporte;
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
                    throw new BusinessException("No se completó la consulta del reporte, favor de intentar nuevamente: " + e.Message);
                }

            }
        }

        public ReporteSubregistroRespuesta ConsultarReporteSexoSubregistro(Collection<string> colAnos, Collection<string> colMeses, Collection<Municipio> colMunicipios)
        {
            ReporteSubregistroRespuesta reporte = new ReporteSubregistroRespuesta();

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

                XmlDocument xmlReporte = reporteDAO.ConsultarReporteSexoSubregistro(anosUnion, mesesUnion, municipiosUnion);
                reporte.XmlReporte = JsonConvert.SerializeXmlNode(xmlReporte);

                Collection<string> cabeceros = new Collection<string>();
                reporte.ColFilas = ObtenerFilas(cabeceros, xmlReporte);
                reporte.ColCabeceros = cabeceros;
                return reporte;
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
                    throw new BusinessException("No se completó la consulta del reporte, favor de intentar nuevamente: " + e.Message);
                }

            }
        }


        public ReporteSubregistroRespuesta ConsultarReporteEscolaridadSubregistro(Collection<string> colAnos, Collection<string> colMeses, Collection<Municipio> colMunicipios)
        {
            ReporteSubregistroRespuesta reporte = new ReporteSubregistroRespuesta();

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

                XmlDocument xmlReporte = reporteDAO.ConsultarReporteEscolaridadSubregistro(anosUnion, mesesUnion, municipiosUnion);
                reporte.XmlReporte = JsonConvert.SerializeXmlNode(xmlReporte);

                Collection<string> cabeceros = new Collection<string>();
                reporte.ColFilas = ObtenerFilas(cabeceros, xmlReporte);
                reporte.ColCabeceros = cabeceros;
                return reporte;
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
                    throw new BusinessException("No se completó la consulta del reporte, favor de intentar nuevamente: " + e.Message);
                }

            }
        }

        public ReporteSubregistroRespuesta ConsultarReporteEdoCivilSubregistro(Collection<string> colAnos, Collection<string> colMeses, Collection<Municipio> colMunicipios)
        {
            ReporteSubregistroRespuesta reporte = new ReporteSubregistroRespuesta();

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

                XmlDocument xmlReporte = reporteDAO.ConsultarReporteEdoCivilSubregistro(anosUnion, mesesUnion, municipiosUnion);
                reporte.XmlReporte = JsonConvert.SerializeXmlNode(xmlReporte);

                Collection<string> cabeceros = new Collection<string>();
                reporte.ColFilas = ObtenerFilas(cabeceros, xmlReporte);
                reporte.ColCabeceros = cabeceros;
                return reporte;
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
                    throw new BusinessException("No se completó la consulta del reporte, favor de intentar nuevamente: " + e.Message);
                }

            }
        }

        internal ReporteSubregistroRespuesta ConsultarReporteSexoSubregistro(object colAnos, object colMeses, object colMunicipios)
        {
            throw new NotImplementedException();
        }

        public ReporteSubregistroRespuesta ConsultarReporteEdadSubregistro(Collection<string> colAnos, Collection<string> colMeses, Collection<Municipio> colMunicipios)
        {
            ReporteSubregistroRespuesta reporte = new ReporteSubregistroRespuesta();

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

                XmlDocument xmlReporte = reporteDAO.ConsultarReporteEdadSubregistro(anosUnion, mesesUnion, municipiosUnion);
                reporte.XmlReporte = JsonConvert.SerializeXmlNode(xmlReporte);

                Collection<string> cabeceros = new Collection<string>();
                reporte.ColFilas = ObtenerFilas(cabeceros, xmlReporte);
                reporte.ColCabeceros = cabeceros;
                return reporte;
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
                    throw new BusinessException("No se completó la consulta del reporte, favor de intentar nuevamente: " + e.Message);
                }

            }
        }

        public ReporteSubregistroRespuesta ConsultarReporteNumNacSubregistro(Collection<string> colAnos, Collection<string> colMeses, Collection<Municipio> colMunicipios)
        {
            ReporteSubregistroRespuesta reporte = new ReporteSubregistroRespuesta();

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

                XmlDocument xmlReporte = reporteDAO.ConsultarReporteNumNacSubregistro(anosUnion, mesesUnion, municipiosUnion);
                reporte.XmlReporte = JsonConvert.SerializeXmlNode(xmlReporte);

                Collection<string> cabeceros = new Collection<string>();
                reporte.ColFilas = ObtenerFilas(cabeceros, xmlReporte);
                reporte.ColCabeceros = cabeceros;
                return reporte;
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
                    throw new BusinessException("No se completó la consulta del reporte, favor de intentar nuevamente: " + e.Message);
                }

            }
        }

        #endregion
        #region Métodos Privados
        public Collection<ReporteFila> ObtenerFilas(Collection<string> cabeceros, XmlDocument xmlDoc)
        {
            Collection<ReporteFila> ColFilas = new Collection<ReporteFila>();

            XmlNodeList nodes = xmlDoc.GetElementsByTagName("Fila");


            XmlNode xmlCabecero = nodes.Item(0);
            foreach (XmlNode childNode in xmlCabecero.ChildNodes)
            {
                cabeceros.Add(childNode.Name.ToString().Replace("A0", " "));
            }


            foreach (XmlNode node in nodes)
            {
                ReporteFila fila = new ReporteFila();
                fila.ColCeldas = new Collection<ReporteCelda>();

                foreach (XmlNode childNode in node.ChildNodes)
                {
                    ReporteCelda celda = new ReporteCelda();
                    celda.NombreCelda = childNode.Name.ToString().Replace("A0", " ");
                    celda.Valor = childNode.InnerText.ToString();
                    fila.ColCeldas.Add(celda);
                }
                ColFilas.Add(fila);
            }
            return ColFilas;
        }
        #endregion
    }
}
