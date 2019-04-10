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
        private const string PRS_OFICILIA = "SDB.PRSOficialia";
        private const string PR_INS_OFICILIA = "SDB.PRInsOficialia";
        private const string PR_U_OFICILIA = "SDB.PRUOficialia";
        private const string PR_U_OFICINA = "SDB.PRUOficina";
        private const string PR_DEL_OFICILIA = "SDB.PRDelOficialia";
        private const string PR_DEL_OFICINA = "SDB.PRDelOficina";
        private const string PRS_OFICINAS = "SDB.PRSOficinas";
        private const string PR_INS_OFICINA = "SDB.PRInsOficina";
        private const string PRS_OFICINA = "SDB.PRSOficina";


        #endregion

        #region Métodos públicos oficinas
        public DataTable ConsultarOficinas(string municipiosUnion)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRS_OFICINAS, CreaParametrosConsultaGeoreferenciacion(municipiosUnion), dataSet);

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

        public bool InsertarOficina(Oficina oficina)
        {
            bool resultado = false;
            try
            {
                EjecutaProcedimiento(PR_INS_OFICINA, CreaParametrosInsertaOficina(oficina));

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

        public Oficina ConsultarOficina(int OId)
        {
            Oficina oficina = new Oficina();
            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRS_OFICINA, CreaParametrosConsultaOficina(OId), dataSet);

                    if (this.Codigo == 0 && ValidaDataSet(dataSet))
                    {
                        foreach (DataRow r in dataSet.Tables[0].Rows)
                        {
                            Oficina o = new Oficina
                            {
                                OId = r.Field<int>("OId"),
                                OficinaId = r.Field<int>("OficinaId"),
                                TipoId = r.Field<int>("TipoId"),
                                TipoDesc = r.Field<string>("TipoDesc"),
                                TipoInstitucion = r.Field<string>("TipoInstitucion"),
                                Latitud = r.Field<string>("Latitud"),
                                Longitud = r.Field<string>("Longitud"),
                                Region = r.Field<string>("Region"),
                                EdoId = r.Field<int>("EdoId"),
                                MpioId = r.Field<int>("MpioId"),
                                MpioDesc = r.Field<string>("MpioDesc"),
                                LocId = r.Field<int>("LocId"),
                                LocDesc = r.Field<string>("LocDesc"),
                                Calle = r.Field<string>("Calle"),
                                Numero = r.Field<string>("Numero"),
                                Colonia = r.Field<string>("Colonia"),
                                CP = r.Field<string>("CP"),
                                EntreCalles = r.Field<string>("EntreCalles"),
                                HorarioAtencion = r.Field<string>("HorarioAtencion"),
                                Telefono = r.Field<string>("Telefono"),
                                OficialNombre = r.Field<string>("OficialNombre"),
                                OficialApellidos = r.Field<string>("OficialApellidos"),
                                CorreoE = r.Field<string>("CorreoE"),
                                InvSerLuz = r.Field<byte>("InvSerLuz"),
                                InvSerAgua = r.Field<byte>("InvSerAgua"),
                                InvLocalPropio = r.Field<byte>("InvLocalPropio"),
                                InvSerSanitario = r.Field<byte>("InvSerSanitario"),
                                InvEscritorios = r.Field<byte>("InvEscritorios"),
                                InvSillas = r.Field<byte>("InvSillas"),
                                InvArchiveros = r.Field<byte>("InvArchiveros"),
                                InvCompPriv = r.Field<byte>("InvCompPriv"),
                                InvCompGob = r.Field<byte>("InvCompGob"),
                                InvEscanPriv = r.Field<byte>("InvEscanPriv"),
                                InvEscanGob = r.Field<byte>("InvEscanGob"),
                                InvImpPriv = r.Field<byte>("InvImpPriv"),
                                EquiNet = r.Field<byte>("EquiNet"),
                                EquiTrabNet = r.Field<byte>("EquiTrabNet"),
                                EquiVentExpress = r.Field<byte>("EquiVentExpress"),
                                EquiConDrc = r.Field<byte>("EquiConDrc"),
                                ExpideCurp = r.Field<byte>("ExpideCurp"),
                                ExpideActasForaneas = r.Field<byte>("ExpideActasForaneas")
                            };

                            oficina = o;
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


            return oficina;
        }

        public bool ActualizarOficina(Oficina oficina)
        {
            bool resultado = false;
            try
            {
                EjecutaProcedimiento(PR_U_OFICINA, CreaParametrosActualizarOficina(oficina));

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

        public bool EliminarOficina(int oId)
        {
            bool resultado = false;
            try
            {
                EjecutaProcedimiento(PR_DEL_OFICINA, CreaParametrosEliminarOficina(oId));

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

        #endregion métodos públicos oficinas

        #region métodos privados Oficinas
        private static Boolean ValidaDataSet(DataSet dataSet)
        {
            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        private static Collection<SqlParameter> CreaParametrosConsultaGeoreferenciacion(string municipiosUnion)
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

        private static Collection<SqlParameter> CreaParametrosInsertaOficina(Oficina oficina)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;
            parametro = new SqlParameter("@pi_oficina_id", SqlDbType.Int)
            {
                Value = oficina.OficinaId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_tipo_id", SqlDbType.Int)
            {
                Value = oficina.TipoId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_tipo_institucion", SqlDbType.NVarChar)
            {
                Size = 60,
                Value = oficina.TipoInstitucion
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_institucion", SqlDbType.NVarChar)
            {
                Size = 30,
                Value = oficina.Institucion
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_latitud", SqlDbType.NVarChar)
            {
                Size = 20,
                Value = oficina.Latitud
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_longitud", SqlDbType.NVarChar)
            {
                Size = 20,
                Value = oficina.Longitud
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_region", SqlDbType.NVarChar)
            {
                Size = 20,
                Value = oficina.Region
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_edo_id", SqlDbType.Int)
            {
                Value = 5
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_mpio_id", SqlDbType.Int)
            {
                Value = oficina.MpioId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_loc_id", SqlDbType.Int)
            {
                Value = oficina.LocId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_calle", SqlDbType.NVarChar)
            {
                Size = 60,
                Value = oficina.Calle
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_numero", SqlDbType.NVarChar)
            {
                Size = 10,
                Value = oficina.Numero
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_colonia", SqlDbType.NVarChar)
            {
                Size = 60,
                Value = oficina.Colonia
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_cp", SqlDbType.NVarChar)
            {
                Size = 5,
                Value = oficina.CP
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_entre_calles", SqlDbType.NVarChar)
            {
                Size = 200,
                Value = oficina.EntreCalles
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_horario_atencion", SqlDbType.NVarChar)
            {
                Size = 50,
                Value = oficina.HorarioAtencion
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_telefono", SqlDbType.NVarChar)
            {
                Size = 25,
                Value = oficina.Telefono
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_oficial_nombre", SqlDbType.NVarChar)
            {
                Size = 80,
                Value = oficina.OficialNombre
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_oficial_apellidos", SqlDbType.NVarChar)
            {
                Size = 80,
                Value = oficina.OficialApellidos
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_oficial_correo_e", SqlDbType.NVarChar)
            {
                Size = 60,
                Value = oficina.CorreoE
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_serv_luz", SqlDbType.Int)
            {
               Value = oficina.InvSerLuz
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_serv_agua", SqlDbType.Int)
            {
                Value = oficina.InvSerAgua
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_local_propio", SqlDbType.Int)
            {
                Value = oficina.InvLocalPropio
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_serv_sanitario", SqlDbType.Int)
            {
                Value = oficina.InvSerSanitario
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_escritorios", SqlDbType.Int)
            {
                Value = oficina.InvEscritorios
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_sillas", SqlDbType.Int)
            {
                Value = oficina.InvSillas
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_archiveros", SqlDbType.Int)
            {
                Value = oficina.InvArchiveros
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_computo_priv", SqlDbType.Int)
            {
                Value = oficina.InvCompPriv
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_computo_gob", SqlDbType.Int)
            {
                Value = oficina.InvCompGob
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_escaner_priv", SqlDbType.Int)
            {
                Value = oficina.InvEscanPriv
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_escaner_gob", SqlDbType.Int)
            {
                Value = oficina.InvEscanGob
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_impresora_priv", SqlDbType.Int)
            {
                Value = oficina.InvImpPriv
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_impresora_gob", SqlDbType.Int)
            {
                Value = oficina.InvImpGob
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_equi_internet", SqlDbType.Int)
            {
                Value = oficina.EquiNet
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_equi_trab_internet", SqlDbType.Int)
            {
                Value = oficina.EquiTrabNet
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_equi_vent_express", SqlDbType.Int)
            {
                Value = oficina.EquiVentExpress
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_equi_con_drc", SqlDbType.Int)
            {
                Value = oficina.EquiConDrc
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_expide_curp", SqlDbType.Int)
            {
                Value = oficina.ExpideCurp
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_expide_actas_foraneas", SqlDbType.Int)
            {
                Value = oficina.ExpideActasForaneas
            };
            parametros.Add(parametro);            

            CreaParametrosSalida(parametros);

            return parametros;
        }
 

        private static Collection<SqlParameter> CreaParametrosConsultaOficina(int OId)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;

            parametro = new SqlParameter("@pi_o_id", SqlDbType.Int)
            {
                Value = OId
            };
            parametros.Add(parametro);

            CreaParametrosSalida(parametros);

            return parametros;
        }



        private static Collection<SqlParameter> CreaParametrosActualizarOficina(Oficina oficina)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;
            parametro = new SqlParameter("@pi_o_id", SqlDbType.Int)
            {
                Value = oficina.OId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_oficina_id", SqlDbType.Int)
            {
                Value = oficina.OficinaId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_tipo_id", SqlDbType.Int)
            {
                Value = oficina.TipoId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_tipo_institucion", SqlDbType.NVarChar)
            {
                Size = 60,
                Value = oficina.TipoInstitucion
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_institucion", SqlDbType.NVarChar)
            {
                Size = 30,
                Value = oficina.Institucion
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_latitud", SqlDbType.NVarChar)
            {
                Size = 20,
                Value = oficina.Latitud
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_longitud", SqlDbType.NVarChar)
            {
                Size = 20,
                Value = oficina.Longitud
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_region", SqlDbType.NVarChar)
            {
                Size = 20,
                Value = oficina.Region
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_edo_id", SqlDbType.Int)
            {
                Value = 5
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_mpio_id", SqlDbType.Int)
            {
                Value = oficina.MpioId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_loc_id", SqlDbType.Int)
            {
                Value = oficina.LocId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_calle", SqlDbType.NVarChar)
            {
                Size = 60,
                Value = oficina.Calle
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_numero", SqlDbType.NVarChar)
            {
                Size = 10,
                Value = oficina.Numero
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_colonia", SqlDbType.NVarChar)
            {
                Size = 60,
                Value = oficina.Colonia
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_cp", SqlDbType.NVarChar)
            {
                Size = 5,
                Value = oficina.CP
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_entre_calles", SqlDbType.NVarChar)
            {
                Size = 200,
                Value = oficina.EntreCalles
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_horario_atencion", SqlDbType.NVarChar)
            {
                Size = 50,
                Value = oficina.HorarioAtencion
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_telefono", SqlDbType.NVarChar)
            {
                Size = 25,
                Value = oficina.Telefono
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_oficial_nombre", SqlDbType.NVarChar)
            {
                Size = 80,
                Value = oficina.OficialNombre
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_oficial_apellidos", SqlDbType.NVarChar)
            {
                Size = 80,
                Value = oficina.OficialApellidos
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_oficial_correo_e", SqlDbType.NVarChar)
            {
                Size = 60,
                Value = oficina.CorreoE
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_serv_luz", SqlDbType.Int)
            {
                Value = oficina.InvSerLuz
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_serv_agua", SqlDbType.Int)
            {
                Value = oficina.InvSerAgua
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_local_propio", SqlDbType.Int)
            {
                Value = oficina.InvLocalPropio
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_serv_sanitario", SqlDbType.Int)
            {
                Value = oficina.InvSerSanitario
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_escritorios", SqlDbType.Int)
            {
                Value = oficina.InvEscritorios
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_sillas", SqlDbType.Int)
            {
                Value = oficina.InvSillas
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_archiveros", SqlDbType.Int)
            {
                Value = oficina.InvArchiveros
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_computo_priv", SqlDbType.Int)
            {
                Value = oficina.InvCompPriv
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_computo_gob", SqlDbType.Int)
            {
                Value = oficina.InvCompGob
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_escaner_priv", SqlDbType.Int)
            {
                Value = oficina.InvEscanPriv
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_escaner_gob", SqlDbType.Int)
            {
                Value = oficina.InvEscanGob
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_impresora_priv", SqlDbType.Int)
            {
                Value = oficina.InvImpPriv
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_inv_impresora_gob", SqlDbType.Int)
            {
                Value = oficina.InvImpGob
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_equi_internet", SqlDbType.Int)
            {
                Value = oficina.EquiNet
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_equi_trab_internet", SqlDbType.Int)
            {
                Value = oficina.EquiTrabNet
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_equi_vent_express", SqlDbType.Int)
            {
                Value = oficina.EquiVentExpress
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_equi_con_drc", SqlDbType.Int)
            {
                Value = oficina.EquiConDrc
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_expide_curp", SqlDbType.Int)
            {
                Value = oficina.ExpideCurp
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_expide_actas_foraneas", SqlDbType.Int)
            {
                Value = oficina.ExpideActasForaneas
            };
            parametros.Add(parametro);

            CreaParametrosSalida(parametros);

            return parametros;
        }

        private static Collection<SqlParameter> CreaParametrosEliminarOficina(int oId)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;
            parametro = new SqlParameter("@pi_o_id", SqlDbType.Int)
            {
                Value = oId
            };
            parametros.Add(parametro);

            CreaParametrosSalida(parametros);

            return parametros;
        }
        #endregion métodos privados Oficinas



        #region métodos públicos oficialias
        public DataTable ConsultarOficialias(string municipiosUnion)
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



        public Oficialia ConsultarOficialia(int OId)
        {
            Oficialia oficialia = new Oficialia();
            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRS_OFICILIA, CreaParametrosConsultaOficialia(OId), dataSet);

                    if (this.Codigo == 0 && ValidaDataSet(dataSet))
                    {
                        foreach (DataRow r in dataSet.Tables[0].Rows)
                        {
                            oficialia = new Oficialia
                            {
                                OId = r.Field<int>("OId"),
                                OficialiaId = r.Field<int>("OficialiaId"),
                                MpioId = r.Field<int>("MpioId"),
                                MpioDesc = r.Field<string>("MpioDesc"),
                                LocId = r.Field<int>("LocId"),
                                LocDesc = r.Field<string>("LocDesc"),
                                Calle = r.Field<string>("Calle"),
                                Numero = r.Field<string>("Numero"),
                                Colonia = r.Field<string>("Colonia"),
                                CP = r.Field<string>("CP"),
                                Telefono = r.Field<string>("Telefono"),
                                Nombres = r.Field<string>("Nombres"),
                                Apellidos = r.Field<string>("Apellidos"),
                                CorreoE = r.Field<string>("CorreoE"),
                                Latitud = r.Field<string>("Latitud"),
                                Longitud = r.Field<string>("Longitud"),
                                Observaciones = r.Field<string>("Observaciones")
                            };
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

            return oficialia;
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

        public bool EliminarOficialia(int oId)
        {
            bool resultado = false;
            try
            {
                EjecutaProcedimiento(PR_DEL_OFICILIA, CreaParametrosEliminarOficialia(oId));

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
        #endregion métodos públicos oficialías

        #region métodos privados oficialías
        private static Collection<SqlParameter> CreaParametrosConsultaOficialia(int OId)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;

            parametro = new SqlParameter("@pi_oid", SqlDbType.Int)
            {
                Value = OId
            };
            parametros.Add(parametro);

            CreaParametrosSalida(parametros);

            return parametros;
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

        private static Collection<SqlParameter> CreaParametrosEliminarOficialia(int oId)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;
            parametro = new SqlParameter("@pi_o_id", SqlDbType.Int)
            {
                Value = oId
            };
            parametros.Add(parametro);            

            CreaParametrosSalida(parametros);

            return parametros;
        }
        #endregion

    }

}