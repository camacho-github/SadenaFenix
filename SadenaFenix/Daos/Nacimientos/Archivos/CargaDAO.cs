using SadenaFenix.Commons.Utilerias;
using SadenaFenix.Excepcions;
using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Models.Catalogos.Tiempo;
using SadenaFenix.Persistence;
using SadenaFenix.Transport.Catalogos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SadenaFenix.Daos.Nacimientos.Archivos
{
    public class CargaDAO : DataContext
    {
        #region Variables de instancia
        private const string CT_LOCALIDAD = "SDB.CTLocalidad";
        private const string TM_SINAC = "SDB.TMSINAC";
        private const string TM_SIC = "SDB.TMSIC";
        private const string CONSULTA_FUENTE_LOCALIDAD = "Select [EDO],[EDO_DESCRIP],[MPO],[MPO_DESCRIP],[LOC],[LOC_DESCRIP] from [CATLOC$A1:I298548]";
        private const string CONSULTA_FUENTE_ACCESS_LOCALIDAD = "Select EDO,EDO_DESCRIP,MPO,MPO_DESCRIP,LOC,LOC_DESCRIP from CATLOC";
        //private const string CONSULTA_FUENTE_SINAC = "SELECT FOLIO, NOMBRE, PATERNO, MATERNO, CURP_M, IIF(VAL(ENT_NACM)>0,VAL(ENT_NACM),99), MPO_NACM,FECH_NACH,HORA_NACH, EDADM, EDOCIVIL, CALLE_RES, NUMEXT_RES, NUMINT_RES,NOMASEN_RES, CODPOS_RES, ENT_RES, MPO_RES, LOC_RES, TEL_RES, NUM_EMB, NUM_NACMTO, NUM_NACVIVO, HIJO_SOBV, HIJO_ANTE, IIF(VIVE_AUN = '1', 1, 0), IIF(VAL(NIV_ESCOL)>1,VAL(NIV_ESCOL),1), OCUPHAB,val(SEXOH) FROM NACIMIENTO";
        private const string CONSULTA_FUENTE_SINAC = "SELECT FOLIO, IIF(VAL(CEDOCVE) > 0 AND ISNUMERIC(ENT_NACM), VAL(ENT_NACM), 99), MPO_NAC, FECH_NACH, HORA_NACH, EDADM, EDOCIVIL, CALLE_RES, NUMEXT_RES, NUMINT_RES, ENT_RES, MPO_RES, LOC_RES, IIF(NUM_NACVIVO IS NULL, 0,NUM_NACVIVO),  IIF(VAL(NIV_ESCOL) > 1, VAL(NIV_ESCOL), 1), OCUPHAB, val(SEXOH) FROM NACIMIENTO";
        private const string CONSULTA_FUENTE_SIC = "Select [EDO_OFI],[MUN_OFI],[OFICIALIA],[ANO],[FECHA_REG],[FECHA_NAC],[LOCALIDAD],[MUNICIPIO],[ESTADO],[PAIS],[NO_CERTIF] from [{0}$A1:DD{1}]";
        private const string RUTA_SERVIDOR = ""; //"C:\\SADENA\\";
        private const string PRN_INS_CONTROL_CARGA = "SDB.PRNInsControlCarga";
        private const string PRDEL_TM_SIC = "SDB.PRDelTMSIC";
        private const string PRDEL_TM_SINAC = "SDB.PRDelTMSINAC";
        private const string PRN_PROCESAR_CARGA_SINAC = "SDB.PRNProcesarCargaSINAC";
        private const string PRN_PROCESAR_CARGA_SIC = "SDB.PRNProcesarCargaSIC";
        private const string PRS_CATALOGOS_PRE_CONSULTA = "SDB.PRSCatalogosPreConsulta";
        private const string PRS_CATALOGOS_SIC_PRE_CONSULTA = "SDB.PRSCatalogosSICPreConsulta";
        private const string PRS_CONSULTAR_PARAMETRO = "SDB.PRSParametro";
        private const string PRS_ACTUALIZAR_PARAMETRO = "SDB.PRUParametro";

        public ParametroRespuesta ConsultarParametroRegistroExtemporaneo()
        {
            ParametroRespuesta p = null;

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRS_CONSULTAR_PARAMETRO, CreaParametrosConsultaParametro(1), dataSet);

                    if (this.Codigo == 0 && ValidaDataSet(dataSet))
                    {
                        DataRow row = dataSet.Tables[0].Rows[0];

                        p = new ParametroRespuesta
                        {
                            ParametroValor = row.Field<int>("ParametroValor")
                        };
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

            return p;
        }

        public bool ActualizarDiasExtemporaneos(int parametroValor)
        {
            bool resultado = false;
            try
            {
                EjecutaProcedimiento(PRS_ACTUALIZAR_PARAMETRO, CreaParametrosActualizarDiasExtemporaneos(1,parametroValor));

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
        #endregion

        #region Métodos Públicos
        public bool PreCargarArchivoLocalidad(string nombreArchivo)
        {
            try
            {
                string consultaFuente = CONSULTA_FUENTE_ACCESS_LOCALIDAD;
                string nombreTabla = CT_LOCALIDAD;
                string nombreCompletoArchivo = RUTA_SERVIDOR + nombreArchivo;
                //GuardarExcelEnBaseDatos(nombreCompletoArchivo, consultaFuente, nombreTabla);
                GuardarAccessEnBaseDatos(nombreCompletoArchivo, consultaFuente, nombreTabla);

            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);
                if (e.Message.Contains("duplicate"))
                {
                    throw new DAOException(2, e.Message);
                }
                
                throw new DAOException(-1, e.Message);
            }


            return true;
        }


        public bool InsertarBitacoraCarga(int sesionId, int identificador, string ano, string nombreArchivo)
        {
            bool resultado = false;
            try
            {
                EjecutaProcedimiento(PRN_INS_CONTROL_CARGA, CreaParametrosInsertaControlCarga(sesionId, identificador, ano, nombreArchivo));

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

        public CatalogosCargasRespuesta ObtenerCatalogosCargas()
        {
            CatalogosCargasRespuesta catalogosCargasRespuesta = new CatalogosCargasRespuesta();

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    Collection<SqlParameter> parametrosConsultaCargas = new Collection<SqlParameter>();
                    CreaParametrosSalida(parametrosConsultaCargas);

                    EjecutaProcedimiento(PRS_CATALOGOS_PRE_CONSULTA, parametrosConsultaCargas, dataSet);

                    if (this.Codigo == 0)
                    {
                        if (dataSet != null && dataSet.Tables.Count > 0
                            && dataSet.Tables[0].Rows.Count > 0)
                        {

                            catalogosCargasRespuesta.ColAnios = new Collection<Anio>();

                            foreach (DataRow r in dataSet.Tables[0].Rows)
                            {
                                Anio anio = new Anio
                                {
                                    AnioId = Convert.ToInt32(r.Field<string>("AnoCarga")),
                                    AnioDesc = r.Field<string>("AnoCarga")
                                };

                                catalogosCargasRespuesta.ColAnios.Add(anio);
                            }
                        }

                        if (dataSet != null && dataSet.Tables.Count > 0
                            && dataSet.Tables[1].Rows.Count > 0)
                        {

                            catalogosCargasRespuesta.ColMeses = new Collection<Mes>();

                            foreach (DataRow r in dataSet.Tables[1].Rows)
                            {
                                Mes mes = new Mes
                                {
                                    MesId = r.Field<int>("MesId"),
                                    MesDesc = r.Field<string>("MesDesc")
                                };

                                catalogosCargasRespuesta.ColMeses.Add(mes);
                            }
                        }

                        if (dataSet != null && dataSet.Tables.Count > 0
                            && dataSet.Tables[2].Rows.Count > 0)
                        {

                            catalogosCargasRespuesta.ColMunicipios = new Collection<Municipio>();

                            foreach (DataRow r in dataSet.Tables[2].Rows)
                            {
                                Municipio mpio = new Municipio
                                {
                                    MpioId = r.Field<int>("MpioId"),
                                    MpioDesc = r.Field<string>("MpioDesc")
                                };

                                catalogosCargasRespuesta.ColMunicipios.Add(mpio);
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

            return catalogosCargasRespuesta;
        }

        //PRS_CATALOGOS_SIC_PRE_CONSULTA

        public CatalogosCargasSICRespuesta ObtenerCatalogosSICCargas()
        {
            CatalogosCargasSICRespuesta catalogosCargasSICRespuesta = new CatalogosCargasSICRespuesta();

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    Collection<SqlParameter> parametrosConsultaCargas = new Collection<SqlParameter>();
                    CreaParametrosSalida(parametrosConsultaCargas);

                    EjecutaProcedimiento(PRS_CATALOGOS_SIC_PRE_CONSULTA, parametrosConsultaCargas, dataSet);

                    if (this.Codigo == 0)
                    {
                        if (dataSet != null && dataSet.Tables.Count > 0
                            && dataSet.Tables[0].Rows.Count > 0)
                        {

                            catalogosCargasSICRespuesta.ColAniosRegistro = new Collection<Anio>();

                            foreach (DataRow r in dataSet.Tables[0].Rows)
                            {
                                Anio anio = new Anio
                                {
                                    AnioId = Convert.ToInt32(r.Field<string>("AnoRegistroSIC")),
                                    AnioDesc = r.Field<string>("AnoRegistroSIC")
                                };

                                catalogosCargasSICRespuesta.ColAniosRegistro.Add(anio);
                            }
                        }

                        if (dataSet != null && dataSet.Tables.Count > 0
                            && dataSet.Tables[1].Rows.Count > 0)
                        {

                            catalogosCargasSICRespuesta.ColAniosNac = new Collection<Anio>();

                            foreach (DataRow r in dataSet.Tables[1].Rows)
                            {
                                Anio anio = new Anio
                                {
                                    AnioId = r.Field<int>("AnoNacSIC"),
                                    AnioDesc = Convert.ToString(r.Field<int>("AnoNacSIC"))
                                };

                                catalogosCargasSICRespuesta.ColAniosNac.Add(anio);
                            }
                        }

                        if (dataSet != null && dataSet.Tables.Count > 0
                            && dataSet.Tables[2].Rows.Count > 0)
                        {

                            catalogosCargasSICRespuesta.ColMeses = new Collection<Mes>();

                            foreach (DataRow r in dataSet.Tables[2].Rows)
                            {
                                Mes mes = new Mes
                                {
                                    MesId = r.Field<int>("MesId"),
                                    MesDesc = r.Field<string>("MesDesc")
                                };

                                catalogosCargasSICRespuesta.ColMeses.Add(mes);
                            }
                        }

                        if (dataSet != null && dataSet.Tables.Count > 0
                            && dataSet.Tables[3].Rows.Count > 0)
                        {

                            catalogosCargasSICRespuesta.ColMunicipios = new Collection<Municipio>();

                            foreach (DataRow r in dataSet.Tables[3].Rows)
                            {
                                Municipio mpio = new Municipio
                                {
                                    MpioId = r.Field<int>("MpioId"),
                                    MpioDesc = r.Field<string>("MpioDesc")
                                };

                                catalogosCargasSICRespuesta.ColMunicipios.Add(mpio);
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

            return catalogosCargasSICRespuesta;
        }

        public bool PreCargarArchivoSINAC(string nombreArchivo)
        {
            try
            {
                string consultaFuente = CONSULTA_FUENTE_SINAC;
                string nombreTabla = TM_SINAC;
                string nombreCompletoArchivo = nombreArchivo;
                DepurarTablaSINACTemporal();
                GuardarAccessEnBaseDatos(nombreCompletoArchivo, consultaFuente, nombreTabla);
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);

                throw new DAOException(-1, e.Message);
            }


            return true;
        }

        public bool PreCargarArchivoSIC(string nombreArchivo)
        {
            try
            {
                string consultaFuente = CONSULTA_FUENTE_SIC;
                string nombreTabla = TM_SIC;
                string nombreCompletoArchivo = nombreArchivo;
                DepurarTablaSICTemporal();
                GuardarExcelEnBaseDatos(nombreCompletoArchivo, consultaFuente, nombreTabla);
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);

                throw new DAOException(-1, e.Message);
            }


            return true;
        }


        public void ProcesarCargaSINAC()
        {
            try
            {
                Collection<SqlParameter> parametrosSalida = new Collection<SqlParameter>();
                CreaParametrosSalida(parametrosSalida);
                EjecutaProcedimiento(PRN_PROCESAR_CARGA_SINAC, parametrosSalida);

                if (this.Codigo == -1)
                {
                    throw new DAOException(-1, this.Mensaje);
                }

            }
            catch (DAOException de)
            {
                Bitacora.Error("Error al ejecutar procedimiento PRN_PROCESAR_CARGA_SINAC " + de.Message);
                throw de;
            }
        }

        public void ProcesarCargaSIC()
        {

            try
            {
                Collection<SqlParameter> parametrosSalida = new Collection<SqlParameter>();
                CreaParametrosSalida(parametrosSalida);
                EjecutaProcedimiento(PRN_PROCESAR_CARGA_SIC, parametrosSalida);

                if (this.Codigo == -1)
                {
                    throw new DAOException(-1, this.Mensaje);
                }

            }
            catch (DAOException de)
            {
                Bitacora.Error("Error al ejecutar procedimiento PRN_PROCESAR_CARGA_SIC " + de.Message);
                throw de;
            }

        }

        #endregion

        #region Métodos Privados
        private void DepurarTablaSINACTemporal()
        {
            try
            {

                Collection<SqlParameter> parametrosSalida = new Collection<SqlParameter>();
                CreaParametrosSalida(parametrosSalida);
                EjecutaProcedimiento(PRDEL_TM_SINAC, parametrosSalida);

                if (this.Codigo == -1)
                {
                    throw new DAOException(-1, this.Mensaje);
                }

            }
            catch (DAOException de)
            {
                Bitacora.Error("Error al ejecutar procedimiento PRDEL_TM_SINAC " + de.Message);
                throw de;
            }

        }

        private void DepurarTablaSICTemporal()
        {
            try
            {

                Collection<SqlParameter> parametrosSalida = new Collection<SqlParameter>();
                CreaParametrosSalida(parametrosSalida);
                EjecutaProcedimiento(PRDEL_TM_SIC, parametrosSalida);

                if (this.Codigo == -1)
                {
                    throw new DAOException(-1, this.Mensaje);
                }

            }
            catch (DAOException de)
            {
                Bitacora.Error("Error al ejecutar procedimiento PRDEL_TM_SIC " + de.Message);
                throw de;
            }

        }

        private static Boolean ValidaDataSet(DataSet dataSet)
        {
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        private static Collection<SqlParameter> CreaParametrosInsertaControlCarga(int sesionId, int identificador, string ano, string nombreArchivo)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;
            parametro = new SqlParameter("@pi_sesion_id", SqlDbType.Int)
            {
                Value = sesionId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_control_tipo_id", SqlDbType.Int)
            {
                Value = identificador
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_ano", SqlDbType.NVarChar)
            {
                Size = 4,
                Value = ano
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_nombre_archivo", SqlDbType.NVarChar)
            {
                Size = 255,
                Value = nombreArchivo
            };
            parametros.Add(parametro);
            
            CreaParametrosSalida(parametros);

            return parametros;
        }

        private static Collection<SqlParameter> CreaParametrosConsultaParametro(int parametroId)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;
            parametro = new SqlParameter("@pi_parametro_id", SqlDbType.Int)
            {
                Value = parametroId
            };            
            parametros.Add(parametro);

            CreaParametrosSalida(parametros);

            return parametros;
        }

        private static Collection<SqlParameter> CreaParametrosActualizarDiasExtemporaneos(int parametroId,int parametroValor)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;
            parametro = new SqlParameter("@pi_parametro_id", SqlDbType.Int)
            {
                Value = parametroId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_parametro_valor", SqlDbType.Int)
            {
                Value = parametroValor
            };
            parametros.Add(parametro);

            CreaParametrosSalida(parametros);

            return parametros;
        }

        #endregion

    }
}
