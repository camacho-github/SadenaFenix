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
using SadenaFenix.Models.Georeferenciacion;

namespace SadenaFenix.Daos.Georeferenciacion
{
    public class GeorefenciacionDAO : DataContext
    {
        #region Variables de instancia
        private const string PRS_OFICILIAS = "SDB.PRSOficialias";
        private const string PR_INS_OFICILIA = "SDB.PRInsOficialia";
        private const string PR_U_OFICILIA = "SDB.PRUOficialia";
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

        public bool InsertarOficialia(Oficialia oficialia)
        {
            bool resultado = false;
            try
            {
                EjecutaProcedimiento(PR_INS_OFICILIA, CreaParametrosInsertaOficialia(oficialia));

                if (this.Codigo == 0)
                {
                    resultado = true;
                }
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);

                throw new DAOException(-1, e.Message);
            }

            return resultado;
        }

        public bool ActualizarOficialia(Oficialia oficialia)
        {
            bool resultado = false;
            try
            {
                EjecutaProcedimiento(PR_U_OFICILIA, CreaParametrosActualizarOficialia(oficialia));

                if (this.Codigo == 0)
                {
                    resultado = true;
                }
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);

                throw new DAOException(-1, e.Message);
            }

            return resultado;
        }

        private static Collection<SqlParameter> CreaParametrosInsertaOficialia(Oficialia oficialia)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;
            parametro = new SqlParameter("@pi_oficialia_id", SqlDbType.Int)
            {
                Value = oficialia.OficialiaId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_mpio_id", SqlDbType.Int)
            {
                Value = oficialia.MpioId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_loc_id", SqlDbType.Int)
            {
                Value = oficialia.LocId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_calle", SqlDbType.NVarChar)
            {
                Size = 60,
                Value = oficialia.Calle
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_numero", SqlDbType.NVarChar)
            {
                Size = 10,
                Value = oficialia.Numero
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_colonia", SqlDbType.NVarChar)
            {
                Size = 60,
                Value = oficialia.Colonia
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_cp", SqlDbType.NVarChar)
            {
                Size = 5,
                Value = oficialia.CP
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_telefono", SqlDbType.NVarChar)
            {
                Size = 15,
                Value = oficialia.Telefono
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_nombres", SqlDbType.NVarChar)
            {
                Size = 80,
                Value = oficialia.Nombres
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_apellidos", SqlDbType.NVarChar)
            {
                Size = 80,
                Value = oficialia.Apellidos
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_correo_e", SqlDbType.NVarChar)
            {
                Size = 60,
                Value = oficialia.CorreoE
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_latitud", SqlDbType.NVarChar)
            {
                Size = 20,
                Value = oficialia.Latitud
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_longitud", SqlDbType.NVarChar)
            {
                Size = 20,
                Value = oficialia.Longitud
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_observaciones", SqlDbType.NVarChar)
            {
                Value = oficialia.Observaciones
            };
            parametros.Add(parametro);

            CreaParametrosSalida(parametros);

            return parametros;
        }

        private static Collection<SqlParameter> CreaParametrosActualizarOficialia(Oficialia oficialia)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;
            parametro = new SqlParameter("@pi_o_id", SqlDbType.Int)
            {
                Value = oficialia.OId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_oficialia_id", SqlDbType.Int)
            {
                Value = oficialia.OficialiaId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_mpio_id", SqlDbType.Int)
            {
                Value = oficialia.MpioId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_loc_id", SqlDbType.Int)
            {
                Value = oficialia.LocId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_calle", SqlDbType.NVarChar)
            {
                Size = 60,
                Value = oficialia.Calle
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_numero", SqlDbType.NVarChar)
            {
                Size = 10,
                Value = oficialia.Numero
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_colonia", SqlDbType.NVarChar)
            {
                Size = 60,
                Value = oficialia.Colonia
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_cp", SqlDbType.NVarChar)
            {
                Size = 5,
                Value = oficialia.CP
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_telefono", SqlDbType.NVarChar)
            {
                Size = 15,
                Value = oficialia.Telefono
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_nombres", SqlDbType.NVarChar)
            {
                Size = 80,
                Value = oficialia.Nombres
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_apellidos", SqlDbType.NVarChar)
            {
                Size = 80,
                Value = oficialia.Apellidos
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_correo_e", SqlDbType.NVarChar)
            {
                Size = 60,
                Value = oficialia.CorreoE
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_latitud", SqlDbType.NVarChar)
            {
                Size = 20,
                Value = oficialia.Latitud
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_longitud", SqlDbType.NVarChar)
            {
                Size = 20,
                Value = oficialia.Longitud
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_observaciones", SqlDbType.NVarChar)
            {
                Value = oficialia.Observaciones
            };
            parametros.Add(parametro);

            CreaParametrosSalida(parametros);

            return parametros;
        }


        #endregion

    }

}