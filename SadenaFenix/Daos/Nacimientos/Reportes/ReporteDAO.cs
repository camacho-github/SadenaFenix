using SadenaFenix.Commons.Utilerias;
using SadenaFenix.Excepcions;
using SadenaFenix.Models.Nacimientos.Consultas;
using SadenaFenix.Models.Nacimientos.Reportes;
using SadenaFenix.Persistence;
using SadenaFenix.Transport.Nacimientos.Reportes;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Sadena.Daos.Nacimientos.Reportes
{
    public class ReporteDAO : DataContext
    {
        #region Variables de instancia        
        private const string PRN_CONSULTA_SUBREGISTRO_NACIMIENTOS = "SDB.PRSSubregistroNacimientos";
        #endregion

        #region Métodos Públicos
        public SubregistroNacimientosRespuesta ConsultaSubregistroNacimientos(string anosUnion, string mesesUnion, string municipiosUnion)
        {
            SubregistroNacimientosRespuesta SubregistroNacimientosRespuesta = new SubregistroNacimientosRespuesta();

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRN_CONSULTA_SUBREGISTRO_NACIMIENTOS, CreaParametrosSubregistroNacimientos(anosUnion, mesesUnion, municipiosUnion), dataSet);

                    if (this.Codigo == 0)
                    {
                        if (dataSet != null && dataSet.Tables.Count > 0
                            && dataSet.Tables[0].Rows.Count > 0)
                        {

                            SubregistroNacimientosRespuesta.ColTotales = new Collection<SubregistroTotal>();

                            foreach (DataRow r in dataSet.Tables[0].Rows)
                            {
                                SubregistroTotal sub = new SubregistroTotal
                                {
                                    NombreGrupo = r.Field<string>("NombreGrupo"),
                                    Total = r.Field<int>("Total")
                                };

                                SubregistroNacimientosRespuesta.ColTotales.Add(sub);
                            }
                        }

                        if (dataSet != null && dataSet.Tables.Count > 0
                            && dataSet.Tables[1].Rows.Count > 0)
                        {

                            SubregistroNacimientosRespuesta.ColSubregistros = new Collection<Subregistro>();

                            foreach (DataRow r in dataSet.Tables[1].Rows)
                            {
                                Subregistro sub = new Subregistro();
                                sub.Folio = r.Field<string>("Folio");
                                sub.FechaNacimiento = r.Field<string>("FechaNacimiento");
                                sub.SexoId = r.Field<int>("SexoId");
                                sub.SexoDesc = r.Field<string>("SexoDesc");
                                sub.EdoId = r.Field<int>("EdoId");
                                sub.MpioId = r.Field<int>("MpioId");
                                sub.LocId = r.Field<int>("LocId");
                                sub.LocDesc = r.Field<string>("LocDesc");
                                sub.Domicilio = r.Field<string>("Domicilio");
                                sub.Edad = r.Field<int>("Edad");
                                sub.NumNacimiento = r.Field<int>("NumNacimiento");
                                sub.Ocupacion = r.Field<string>("Ocupacion");
                                sub.EdoCivilId = r.Field<int>("EdoCivilId");
                                sub.EdoCivilDesc = r.Field<string>("EdoCivilDesc");
                                sub.EscolId = r.Field<int>("EscolId");
                                sub.EscolDesc = r.Field<string>("EscolDesc");

                                SubregistroNacimientosRespuesta.ColSubregistros.Add(sub);
                            }
                        }

                        if (dataSet != null && dataSet.Tables.Count > 0
                            && dataSet.Tables[2].Rows.Count > 0)
                        {

                            SubregistroNacimientosRespuesta.ColOportunos = new Collection<Subregistro>();

                            foreach (DataRow r in dataSet.Tables[2].Rows)
                            {
                                Subregistro sub = new Subregistro();
                                sub.Folio = r.Field<string>("Folio");
                                sub.FechaNacimiento = r.Field<string>("FechaNacimiento");
                                sub.SexoId = r.Field<int>("SexoId");
                                sub.SexoDesc = r.Field<string>("SexoDesc");
                                sub.EdoId = r.Field<int>("EdoId");
                                sub.MpioId = r.Field<int>("MpioId");
                                sub.LocId = r.Field<int>("LocId");
                                sub.LocDesc = r.Field<string>("LocDesc");
                                sub.Domicilio = r.Field<string>("Domicilio");
                                sub.Edad = r.Field<int>("Edad");
                                sub.NumNacimiento = r.Field<int>("NumNacimiento");
                                sub.Ocupacion = r.Field<string>("Ocupacion");
                                sub.EdoCivilId = r.Field<int>("EdoCivilId");
                                sub.EdoCivilDesc = r.Field<string>("EdoCivilDesc");
                                sub.EscolId = r.Field<int>("EscolId");
                                sub.EscolDesc = r.Field<string>("EscolDesc");

                                SubregistroNacimientosRespuesta.ColOportunos.Add(sub);
                            }
                        }

                        if (dataSet != null && dataSet.Tables.Count > 0
                            && dataSet.Tables[3].Rows.Count > 0)
                        {

                            SubregistroNacimientosRespuesta.ColExtemporaneos = new Collection<Subregistro>();

                            foreach (DataRow r in dataSet.Tables[3].Rows)
                            {
                                Subregistro sub = new Subregistro();
                                sub.Folio = r.Field<string>("Folio");
                                sub.FechaNacimiento = r.Field<string>("FechaNacimiento");
                                sub.SexoId = r.Field<int>("SexoId");
                                sub.SexoDesc = r.Field<string>("SexoDesc");
                                sub.EdoId = r.Field<int>("EdoId");
                                sub.MpioId = r.Field<int>("MpioId");
                                sub.LocId = r.Field<int>("LocId");
                                sub.LocDesc = r.Field<string>("LocDesc");
                                sub.Domicilio = r.Field<string>("Domicilio");
                                sub.Edad = r.Field<int>("Edad");
                                sub.NumNacimiento = r.Field<int>("NumNacimiento");
                                sub.Ocupacion = r.Field<string>("Ocupacion");
                                sub.EdoCivilId = r.Field<int>("EdoCivilId");
                                sub.EdoCivilDesc = r.Field<string>("EdoCivilDesc");
                                sub.EscolId = r.Field<int>("EscolId");
                                sub.EscolDesc = r.Field<string>("EscolDesc");

                                SubregistroNacimientosRespuesta.ColExtemporaneos.Add(sub);
                            }
                        }

                    }
                    else
                    {
                        throw new EmptyDataException(this.Mensaje);
                    }
                }
            }
            catch (Exception de)
            {
                Bitacora.Error(de.Message);
                if (de is EmptyDataException)
                {
                    throw new DAOException(1, de.Message);
                }
                throw new DAOException(-1, de.Message);
            }

            return SubregistroNacimientosRespuesta;
        }

        #endregion

        #region Métodos Privados

        private static Boolean ValidaDataSet(DataSet dataSet)
        {
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        private Collection<SqlParameter> CreaParametrosSubregistroNacimientos(string anosUnion, string mesesUnion, string municipiosUnion)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;

            parametro = new SqlParameter("@pc_anos", SqlDbType.NVarChar)
            {
                Size = 255,
                Value = (string.IsNullOrEmpty(anosUnion)) ? (object)DBNull.Value : anosUnion
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_meses", SqlDbType.NVarChar)
            {
                Size = 255,
                Value = (string.IsNullOrEmpty(mesesUnion)) ? (object)DBNull.Value : mesesUnion
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_municipios", SqlDbType.NVarChar)
            {
                Size = 255,
                Value = (string.IsNullOrEmpty(municipiosUnion)) ? (object)DBNull.Value : municipiosUnion
            };
            parametros.Add(parametro);

            CreaParametrosSalida(parametros);

            return parametros;
        }


        #endregion

    }
}
