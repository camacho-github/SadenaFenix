using SadenaFenix.Commons.Utilerias;
using SadenaFenix.Excepcions;
using SadenaFenix.Models.Usuarios;
using SadenaFenix.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SadenaFenix.Daos.Usuarios
{
    public class UsuarioDAO : DataContext
    {
        #region Variables de instancia
        private const string PRN_INICIAR_SESION = "SDB.PRNIniciarSesion";
        private const string PRN_FINALIZAR_SESION = "SDB.PRNFinalizarSesion";
        private const string PRS_SESION_ACTIVA = "SDB.PRSSesionActiva";
        private const string PRS_USUARIOS = "SDB.PRSUsuarios";
        private const string PR_INS_USUARIO = "SDB.PRIUsuario";
        private const string PRS_USUARIO = "SDB.PRSUsuario";
        private const string PR_U_USUARIO = "SDB.PRUUsuario";
        private const string PR_DEL_USUARIO = "SDB.PRDelUsuario";
        private const string PRS_BIUSUARIOS_SESION = "SDB.PRSBIUsuarioSesion";


        #endregion

        #region Métodos Públicos
        public Usuario IniciarSesion(string identificador, string contrasena, string ip)
        {

            Usuario u = null;

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRN_INICIAR_SESION, CreaParametrosIniciarSesion(identificador, contrasena, ip), dataSet);

                    if (this.Codigo == 0 && ValidaDataSet(dataSet))
                    {
                        DataRow row = dataSet.Tables[0].Rows[0];
                        u = new Usuario()
                        {
                            SesionId = row.Field<int>("SesionId"),
                            UsuarioId = row.Field<int>("UsuarioId"),
                            UsuarioDesc = row.Field<string>("UsuarioDesc"),
                            CorreoE = row.Field<string>("CorreoE")
                        };
                        Rol rol = new Rol
                        {
                            RolId = row.Field<int>("RolId"),
                            RolDesc = row.Field<string>("RolDesc")
                        };
                        u.Rol = rol;
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

            return u;
        }

        public bool InsertarUsuario(UsuarioAlta usuario)
        {
            bool resultado = false;
            try
            {
                EjecutaProcedimiento(PR_INS_USUARIO, CreaParametrosInsertaUsuario(usuario));

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

        public UsuarioAlta ConsultarUsuario(int usuarioId)
        {
            UsuarioAlta u;
            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRS_USUARIO, CreaParametrosConsultaUsuario(usuarioId), dataSet);

                    if (this.Codigo == 0 && ValidaDataSet(dataSet))
                    {
                        DataRow r = dataSet.Tables[0].Rows[0];

                        u = new UsuarioAlta
                        {
                            UsuarioId = r.Field<int>("UsuarioId"),
                            UsuarioDesc = r.Field<string>("UsuarioDesc"),
                            Contrasenia = r.Field<string>("Contrasenia"),
                            CorreoE = r.Field<string>("CorreoE"),
                            RolId = r.Field<int>("RolId"),
                            RolDesc = r.Field<string>("RolDesc")

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


            return u;
        }

        public DataTable ConsultarBitacoraUsuarios()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    Collection<SqlParameter> parametrosUsuarios = new Collection<SqlParameter>();
                    CreaParametrosSalida(parametrosUsuarios);

                    EjecutaProcedimiento(PRS_BIUSUARIOS_SESION, parametrosUsuarios, dataSet);

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

        public bool EliminarUsuario(int usuarioId)
        {
            bool resultado = false;
            try
            {
                EjecutaProcedimiento(PR_DEL_USUARIO, CreaParametrosEliminarUsuario(usuarioId));

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

        public bool ActualizarUsuario(UsuarioAlta usuario)
        {
            bool resultado = false;
            try
            {
                EjecutaProcedimiento(PR_U_USUARIO, CreaParametrosActualizarUsuario(usuario));

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

        public DataTable ConsultarUsuarios()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    Collection<SqlParameter> parametrosUsuarios = new Collection<SqlParameter>();
                    CreaParametrosSalida(parametrosUsuarios);

                    EjecutaProcedimiento(PRS_USUARIOS, parametrosUsuarios, dataSet);

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

        public Usuario FinalizarSesion(int sesionId)
        {

            Usuario u = null;

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRN_FINALIZAR_SESION, CreaParametrosConsultaSesion(sesionId), dataSet);

                    if (this.Codigo == 0 && ValidaDataSet(dataSet))
                    {
                        DataRow row = dataSet.Tables[0].Rows[0];
                        u = new Usuario
                        {
                            SesionId = row.Field<int>("SesionId"),
                            UsuarioId = row.Field<int>("UsuarioId"),
                            UsuarioDesc = row.Field<string>("UsuarioDesc"),
                            CorreoE = row.Field<string>("CorreoE")
                        };
                        Rol rol = new Rol
                        {
                            RolId = row.Field<int>("RolId"),
                            RolDesc = row.Field<string>("RolDesc")
                        };
                        u.Rol = rol;
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

            return u;
        }


        public void ConsultarSesionActiva(int sesionId)
        {
            try
            {

                EjecutaProcedimiento(PRS_SESION_ACTIVA, CreaParametrosConsultaSesion(sesionId));

                if (this.Codigo == 1)
                {
                    throw new BusinessException(1, this.Mensaje);
                }
                else if (this.Codigo == -1)
                {
                    throw new DAOException(-1, this.Mensaje);
                }

            }
            catch (DAOException de)
            {
                Bitacora.Error("Error al ejecutar SDB.PRSSesionActiva " + de.Message);
                throw de;
            }

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

        private static Collection<SqlParameter> CreaParametrosIniciarSesion(string identificador, string contrasena, string ip)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;
            parametro = new SqlParameter("@pc_identificador", SqlDbType.NVarChar)
            {
                Size = 60,
                Value = identificador
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_contrasena", SqlDbType.NVarChar)
            {
                Size = 40,
                Value = contrasena
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_ip", SqlDbType.NVarChar)
            {
                Size = 40,
                Value = ip
            };
            parametros.Add(parametro);

            CreaParametrosSalida(parametros);

            return parametros;
        }

        private static Collection<SqlParameter> CreaParametrosConsultaSesion(int sesionId)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = new SqlParameter("@pi_sesion_id", SqlDbType.Int)
            {
                Value = sesionId
            };
            parametros.Add(parametro);

            CreaParametrosSalida(parametros);

            return parametros;
        }

        private static Collection<SqlParameter> CreaParametrosConsultaUsuario(int usuarioId)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = new SqlParameter("@pi_usuario_id", SqlDbType.Int)
            {
                Value = usuarioId
            };
            parametros.Add(parametro);

            CreaParametrosSalida(parametros);

            return parametros;
        }

        private static Collection<SqlParameter> CreaParametrosInsertaUsuario(UsuarioAlta usuario)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;
            parametro = new SqlParameter("@pc_usuario", SqlDbType.NVarChar)
            {
                Size = 40,
                Value = usuario.UsuarioDesc
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_correo_e", SqlDbType.NVarChar)
            {
                Size = 60,
                Value = usuario.CorreoE
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_rol_id", SqlDbType.Int)
            {
                Value = usuario.RolId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_estatus_id", SqlDbType.Int)
            {
                Value = 1
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_contrasena", SqlDbType.NVarChar)
            {
                Size = 40,
                Value = usuario.Contrasenia
            };
            parametros.Add(parametro);
            
            CreaParametrosSalida(parametros);

            return parametros;
        }
        private static Collection<SqlParameter> CreaParametrosEliminarUsuario(int usuarioId)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;
            parametro = new SqlParameter("@pi_usuario_id", SqlDbType.Int)
            {
                Value = usuarioId
            };
            parametros.Add(parametro);

            CreaParametrosSalida(parametros);

            return parametros;
        }

        private static Collection<SqlParameter> CreaParametrosActualizarUsuario(UsuarioAlta usuario)
        {
            Collection<SqlParameter> parametros = new Collection<SqlParameter>();
            SqlParameter parametro = null;
            parametro = new SqlParameter("@pi_usuario_id", SqlDbType.Int)
            {
               Value = usuario.UsuarioId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_usuario", SqlDbType.NVarChar)
            {
                Size = 40,
                Value = usuario.UsuarioDesc
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_correo_e", SqlDbType.NVarChar)
            {
                Size = 60,
                Value = usuario.CorreoE
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_rol_id", SqlDbType.Int)
            {
                Value = usuario.RolId
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pi_estatus_id", SqlDbType.Int)
            {
                Value = 1
            };
            parametros.Add(parametro);

            parametro = new SqlParameter("@pc_contrasena", SqlDbType.NVarChar)
            {
                Size = 40,
                Value = usuario.Contrasenia
            };
            parametros.Add(parametro);

            CreaParametrosSalida(parametros);

            return parametros;
        }

        #endregion

    }
}
