using System;
using System.Collections.ObjectModel;
using System.Data;
using Newtonsoft.Json;
using SadenaFenix.Commons.Utilerias;
using SadenaFenix.Daos.Catalogos;
using SadenaFenix.Daos.Usuarios;
using SadenaFenix.Excepcions;
using SadenaFenix.Models.Usuarios;
using SadenaFenix.Transport.Usuarios.Acceso;

namespace SadenaFenix.Business.Usuarios
{
    public class UsuarioBLL
    {
        #region Variables de Instancia
        private UsuarioDAO usuarioDAO;
        #endregion

        #region Constructor
        public UsuarioBLL()
        {
            usuarioDAO = new UsuarioDAO();
        }
        #endregion

        #region Métodos Públicos

        public SesionRespuesta IniciarSesion(string identificador, string contrasena, string ip)
        {            
            Usuario usuario = usuarioDAO.IniciarSesion(identificador, contrasena, ip);
            usuario.Json =  JsonConvert.SerializeObject(usuario);

            SesionRespuesta response = new SesionRespuesta
            {
                Usuario = usuario
            };

            return response;
        }

        public SesionRespuesta FinalizarSesion(int sesionId)
        {
            SesionRespuesta response = new SesionRespuesta
            {
                Usuario = usuarioDAO.FinalizarSesion(sesionId)
            };

            return response;
        }

        public void ConsultarSesionActiva(int sesionId)
        {
            try
            {
                usuarioDAO.ConsultarSesionActiva(sesionId);
            }
            catch (DAOException e)
            {
                Bitacora.Error("Error al ejecutar método:ConsultaSesionActiva " + e.Message);
                throw new BusinessException(1, "Ocurrió un error al ejecutar el servicio, favor de intentarlo nuevamente");
            }
        }

        public ConsultarUsuariosRespuesta ConsultarUsuarios()
        {
            ConsultarUsuariosRespuesta respuesta = new ConsultarUsuariosRespuesta();
            DataTable tbUsuarios = usuarioDAO.ConsultarUsuarios();
            
            respuesta.DTUsuarios = tbUsuarios;
            
            return respuesta;
        }

        public bool InsertarUsuario(UsuarioAlta usuario)
        {
            bool resultado = false;
            try
            {
                resultado = usuarioDAO.InsertarUsuario(usuario);

                if(!resultado)
                {
                    throw new Exception("El usuario no fue registrado correctamente, favor de validar los datos: ");
                }
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);
                throw new BusinessException(1,"El usuario no fue registrado correctamente, favor de validar los datos: " + e.Message);
            }

            return resultado;
        }

        public ConsultarUsuarioRespuesta ConsultarUsuario(int usuarioId)
        {
            ConsultarUsuarioRespuesta respuesta = new ConsultarUsuarioRespuesta();
            try
            {
                UsuarioAlta usuario = usuarioDAO.ConsultarUsuario(usuarioId);
                respuesta.Usuario = usuario;
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);
                throw new BusinessException("El usuario no fue obtenido correctamente, favor de intentar nuevamente: " + e.Message);
            }

            return respuesta;
        }

        public bool ActualizarUsuario(UsuarioAlta usuario)
        {
            bool resultado = false;
            try
            {
                resultado = usuarioDAO.ActualizarUsuario(usuario);

                if (!resultado)
                {
                    throw new Exception("El usuario no fue actualizado correctamente, favor de validar los datos: ");
                }
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);
                throw new BusinessException(1, "El usuario no fue actualizado correctamente, favor de validar los datos: " + e.Message);
            }

            return resultado;
        }

        public bool EliminarUsuario(int usuarioId)
        {
            try
            {
                usuarioDAO.EliminarUsuario(usuarioId);
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);
                throw new BusinessException("El usuario no fue eliminado correctamente, favor de intentar nuevamente: " + e.Message);
            }

            return true;
        }

        public ConsultarUsuariosRespuesta ConsultarBitacoraUsuarios()
        {
            ConsultarUsuariosRespuesta respuesta = new ConsultarUsuariosRespuesta();
            try
            {
                DataTable tbUsuarios = usuarioDAO.ConsultarBitacoraUsuarios();
                respuesta.DTUsuarios = tbUsuarios;
            }
            catch (DAOException e)
            {
                Bitacora.Error(e.Message);
                throw new BusinessException(e.Codigo,"No pudo ser obtenida una bitácora, favor de intentar nuevamente: " + e.Message);
            }

            return respuesta;
        }

        #endregion
    }
}
