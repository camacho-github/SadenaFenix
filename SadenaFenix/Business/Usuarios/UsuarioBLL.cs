using SadenaFenix.Commons.Utilerias;
using SadenaFenix.Daos;
using SadenaFenix.Excepcions;
using SadenaFenix.Transport.Usuarios.Acceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SadenaFenix.Business
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
            SesionRespuesta response = new SesionRespuesta
            {
                Usuario = usuarioDAO.IniciarSesion(identificador, contrasena, ip)
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
        #endregion
    }
}
