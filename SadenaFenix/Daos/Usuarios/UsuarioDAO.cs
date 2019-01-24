﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sadena.Daos
{
    public class UsuarioDAO : DataContext
    {
        #region Variables de instancia
        private const string PRN_INICIAR_SESION = "SDB.PRNIniciarSesion";
        private const string PRN_FINALIZAR_SESION = "SDB.PRNFinalizarSesion";
        private const string PRS_SESION_ACTIVA = "SDB.PRSSesionActiva";


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

                    if (this.Codigo == 0 && validaDataSet(dataSet))
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

        public Usuario FinalizarSesion(int sesionId)
        {

            Usuario u = null;

            try
            {
                using (DataSet dataSet = new DataSet())
                {
                    dataSet.Locale = CultureInfo.InvariantCulture;

                    EjecutaProcedimiento(PRN_FINALIZAR_SESION, CreaParametrosConsultaSesion(sesionId), dataSet);

                    if (this.Codigo == 0 && validaDataSet(dataSet))
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

        private static Boolean validaDataSet(DataSet dataSet)
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

        #endregion

    }
}
