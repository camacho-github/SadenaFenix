using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using SadenaFenix.Persistence;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using SadenaFenix.Excepcions;
using SadenaFenix.Commons.Utilerias;

namespace SadenaFenix.Daos.Georeferenciacion
{
    public class GeorefenciacionDAO : DataContext
    {
        #region Variables de instancia
        private const string PRS_OFICILIAS = "SDB.PRSOficialias";
        #endregion

        #region Métodos públicos
        public DataTable ConsultarOficialia(string municipiosUnion)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRS_OFICILIAS, CreaParametrosConsultaGeoreferenciacion(municipiosUnion), dataSet);

                    if (this.Codigo == 0 && ValidaDataSet(dataSet))
                    {
                        dataTable = dataSet.Tables[0];
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

            return dataTable; 
        }
        #endregion

        #region Métodos privados
        private static Boolean ValidaDataSet(DataSet dataSet)
        {
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        private Collection<SqlParameter> CreaParametrosConsultaGeoreferenciacion(string municipiosUnion)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;

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