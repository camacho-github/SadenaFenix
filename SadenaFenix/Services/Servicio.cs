using SadenaFenix.Business.Catalogos;
using SadenaFenix.Business.Nacimientos.Archivos;
using SadenaFenix.Business.Nacimientos.Reportes;
using SadenaFenix.Business.Usuarios;
using SadenaFenix.Excepcions;
using SadenaFenix.Models.Nacimientos.Archivos;
using SadenaFenix.Transport.Catalogos;
using SadenaFenix.Transport.Nacimientos.Archivos;
using SadenaFenix.Transport.Nacimientos.Reportes;
using SadenaFenix.Transport.Usuarios.Acceso;
using System;
using System.Collections.Generic;

namespace SadenaFenix.Services
{
    public class Servicio
    {
        #region Metodos Públicos       
        public SesionRespuesta IniciarSesion(SesionPeticion peticion)
        {
            SesionRespuesta respuesta = new SesionRespuesta();

            try
            {
                UsuarioBLL usuarioBLL = new UsuarioBLL();
                respuesta = usuarioBLL.IniciarSesion(peticion.Identificador, peticion.Contrasena, peticion.IP);
                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", respuesta.Cabecero);
            }
            catch (DAOException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }


            return respuesta;
        }

        public ConsultarUsuariosRespuesta ConsultarUsuarios(ConsultaUsuariosPeticion peticion)
        {
            ConsultarUsuariosRespuesta respuesta = new ConsultarUsuariosRespuesta();
            try
            {
                int sesionId = peticion.Cabecero.SesionId;
                UsuarioBLL usuarioBLL = new UsuarioBLL();
                usuarioBLL.ConsultarSesionActiva(sesionId);

                respuesta = usuarioBLL.ConsultarUsuarios();
            }
            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;
        }

        public ParametroRespuesta ConsultarParametroRegistroExtemporaneo(CabeceroPeticion peticion)
        {
            ParametroRespuesta respuesta = new ParametroRespuesta();           

            try
            {
                int sesionId = peticion.SesionId;
                UsuarioBLL usuarioBLL = new UsuarioBLL();
                usuarioBLL.ConsultarSesionActiva(sesionId);

                CargaBLL cargaBLL = new CargaBLL();
                respuesta = cargaBLL.ConsultarParametroRegistroExtemporaneo();
            }
            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;

        }

        public InsertarUsuarioRespuesta InsertarUsuario(InsertarUsuarioPeticion peticion)
        {
            InsertarUsuarioRespuesta respuesta = new InsertarUsuarioRespuesta();
            try
            {
                UsuarioBLL usuarioBLL = new UsuarioBLL();
                usuarioBLL.InsertarUsuario(peticion.Usuario);
                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", respuesta.Cabecero);
            }
            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;
        }

        public ActualizarParametroRespuesta ActualizarDiasExtemporaneos(ActualizarParametroPeticion peticion)
        {
            ActualizarParametroRespuesta respuesta = new ActualizarParametroRespuesta();

            try
            {
                CargaBLL cargaBLL = new CargaBLL();
                cargaBLL.ActualizarDiasExtemporaneos(peticion.ParametroValor);
            }
            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;
        }

        public AnalisisSICRespuesta ConsultarAnalisisInformacionSIC(AnalisisSICPeticion peticion)
        {
            AnalisisSICRespuesta respuesta = new AnalisisSICRespuesta();
            try
            {
                ReportesBLL reportesBLL = new ReportesBLL();
                respuesta = reportesBLL.ConsultarAnalisisInformacionSIC(peticion.ColAnos, peticion.ColMeses, peticion.ColMunicipios);
                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", respuesta.Cabecero);
            }
            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;
        }

        public SesionRespuesta FinalizarSesion(SesionPeticion peticion)
        {
            SesionRespuesta respuesta = new SesionRespuesta();

            try
            {
                UsuarioBLL usuarioBLL = new UsuarioBLL();
                respuesta = usuarioBLL.FinalizarSesion(peticion.Cabecero.SesionId);
                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", respuesta.Cabecero);
            }
            catch (DAOException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }


            return respuesta;
        }

        
        public CabeceroRespuesta PreCargarDatos(PreCargaPeticion peticion)
        {
            CabeceroRespuesta response = new CabeceroRespuesta();
            try
            {
                int sesionId = peticion.Cabecero.SesionId;

                UsuarioBLL usuarioBLL = new UsuarioBLL();
                usuarioBLL.ConsultarSesionActiva(sesionId);

                List<Archivo> archivoLista = new List<Archivo>(peticion.ColArchivo);
                CargaBLL cargaBLL = new CargaBLL();
                cargaBLL.PreCargarDatos(sesionId, archivoLista);

                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", response);
            }

            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, response);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, response);
            }
            return response;
        }
             

        public CabeceroRespuesta ProcesarCarga(PreCargaPeticion peticion)
        {
            CabeceroRespuesta response = new CabeceroRespuesta();
            try
            {
                int sesionId = peticion.Cabecero.SesionId;

                UsuarioBLL usuarioBLL = new UsuarioBLL();
                usuarioBLL.ConsultarSesionActiva(sesionId);

                CargaBLL cargaBLL = new CargaBLL();
                cargaBLL.ProcesarCarga();

                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", response);
            }

            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, response);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, response);
            }
            return response;
        }


        public CatalogosCargasRespuesta ObtenerCatalogosCargas(CabeceroPeticion peticion)
        {
            CatalogosCargasRespuesta response = new CatalogosCargasRespuesta();
            try
            {
                CargaBLL cargaBLL = new CargaBLL();
                response = cargaBLL.ObtenerCatalogosCargas();

                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", response.Cabecero);
            }

            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, response.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, response.Cabecero);
            }
            return response;
        }

        public TotalesSubregistroNacimientosRespuesta ConsultaTotalesSubregistroNacimientos(SubregistroPeticion Request)
        {
            TotalesSubregistroNacimientosRespuesta response = new TotalesSubregistroNacimientosRespuesta();
            try
            {
                ReportesBLL reportesBLL = new ReportesBLL();

                response = reportesBLL.ConsultaTotalesSubregistroNacimientos(Request.ColAnos, Request.ColMeses, Request.ColMunicipios);
                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", response.Cabecero);
            }

            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, response.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, response.Cabecero);
            }
            return response;
        }

        public SubregistroNacimientosRespuesta ConsultaSubregistroNacimientos(SubregistroPeticion Request)
        {
            SubregistroNacimientosRespuesta response = new SubregistroNacimientosRespuesta();
            try
            {
                ReportesBLL reportesBLL = new ReportesBLL();

                response = reportesBLL.ConsultaSubregistroNacimientos(Request.ColAnos, Request.ColMeses, Request.ColMunicipios);
                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", response.Cabecero);
            }

            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, response.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, response.Cabecero);
            }
            return response;
        }

        public ConsultaMesesRespuesta ConsultarMesesXAnio(ConsultaMesesPeticion Request)
        {
            ConsultaMesesRespuesta response = new ConsultaMesesRespuesta();
            try
            {
                CatalogosBLL catalogosBLL = new CatalogosBLL();
                response = catalogosBLL.ConsultarMesesXAnio(Request.Anio);

                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", response.Cabecero);
            }

            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, response.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, response.Cabecero);
            }
            return response;
        }

        public CatalogosSocioEconomicaRespuesta ConsultarCatalogosSocioeconomica(CabeceroPeticion peticion)
        {
            CatalogosSocioEconomicaRespuesta respuesta = new CatalogosSocioEconomicaRespuesta();
            try
            {
                CatalogosBLL catalogosBLL = new CatalogosBLL();
                respuesta = catalogosBLL.ConsultarCatalogosSocioeconomica();

                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", respuesta.Cabecero);
            }

            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;
        }

        public CatalogosGeografiaRespuesta ConsultarCatalogosGeografia(CabeceroPeticion peticion)
        {
            CatalogosGeografiaRespuesta respuesta = new CatalogosGeografiaRespuesta();
            try
            {
                CatalogosBLL catalogosBLL = new CatalogosBLL();
                respuesta = catalogosBLL.ConsultarCatalogosGeografia();

                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", respuesta.Cabecero);
            }

            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;
        }

        public CatalogoMunicipioRespuesta ConsultarCatalogoMunicipioGeografia(CabeceroPeticion peticion)
        {
            CatalogoMunicipioRespuesta respuesta = new CatalogoMunicipioRespuesta();
            try
            {
                CatalogosBLL catalogosBLL = new CatalogosBLL();
                respuesta = catalogosBLL.ConsultarCatalogoMunicipio();

                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", respuesta.Cabecero);
            }

            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;
        }

        public CatalogoLocalidadRespuesta ConsultarCatalogoLocalidadGeografia(CabeceroPeticion peticion)
        {
            CatalogoLocalidadRespuesta respuesta = new CatalogoLocalidadRespuesta();
            try
            {
                CatalogosBLL catalogosBLL = new CatalogosBLL();
                respuesta = catalogosBLL.ConsultarCatalogoLocalidad();

                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", respuesta.Cabecero);
            }

            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;
        }

        public CatalogoLocalidadRespuesta ConsultarCatalogoLocalidadGeografiaCoahuila(CabeceroPeticion peticion)
        {
            CatalogoLocalidadRespuesta respuesta = new CatalogoLocalidadRespuesta();
            try
            {
                CatalogosBLL catalogosBLL = new CatalogosBLL();
                respuesta = catalogosBLL.ConsultaCatLocalidadCoahuila();

                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", respuesta.Cabecero);
            }

            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;
        }

        public CatalogoRolesRespuesta ConsultarCatalogoRoles(CabeceroPeticion peticion)
        {
            CatalogoRolesRespuesta respuesta = new CatalogoRolesRespuesta();
            try
            {
                CatalogosBLL catalogosBLL = new CatalogosBLL();
                respuesta = catalogosBLL.ConsultarCatRoles();

                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", respuesta.Cabecero);
            }

            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;
        }

        public ReporteSubregistroRespuesta ConsultarReporteTotalesSubregistro(ReporteTotalesSubregistroPeticion peticion)
        {
            ReporteSubregistroRespuesta respuesta = new ReporteSubregistroRespuesta();
            try
            {
                ReportesBLL reportesBLL = new ReportesBLL();
                respuesta = reportesBLL.ConsultarReporteTotalesSubregistro(peticion.ColAnos, peticion.ColMeses, peticion.ColMunicipios);
                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", respuesta.Cabecero);
            }
            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;

        }
        public TotalesMunicipiosRespuesta ConsultarReporteTotalesMunicipio(ReporteTotalesSubregistroPeticion peticion)
        {
            TotalesMunicipiosRespuesta respuesta = new TotalesMunicipiosRespuesta();
            try
            {
                ReportesBLL reportesBLL = new ReportesBLL();
                respuesta = reportesBLL.ConsultarReporteTotalesMunicipio(peticion.ColAnos, peticion.ColMeses, peticion.ColMunicipios);
                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", respuesta.Cabecero);
            }
            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;

        }

        

        public ReporteSubregistroRespuesta ConsultarReporteSexoSubregistro(ReporteSexoSubregistroPeticion peticion)
        {
            ReporteSubregistroRespuesta respuesta = new ReporteSubregistroRespuesta();
            try
            {
                ReportesBLL reportesBLL = new ReportesBLL();
                respuesta = reportesBLL.ConsultarReporteSexoSubregistro(peticion.ColAnos, peticion.ColMeses, peticion.ColMunicipios);
                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", respuesta.Cabecero);
            }
            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;

        }

        public ReporteSubregistroRespuesta ConsultarReporteEdoCivilSubregistro(ReporteEdoCivilSubregistroPeticion peticion)
        {
            ReporteSubregistroRespuesta respuesta = new ReporteSubregistroRespuesta();
            try
            {
                ReportesBLL reportesBLL = new ReportesBLL();
                respuesta = reportesBLL.ConsultarReporteEdoCivilSubregistro(peticion.ColAnos, peticion.ColMeses, peticion.ColMunicipios);
                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", respuesta.Cabecero);
            }
            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;

        }

        public ReporteSubregistroRespuesta ConsultarReporteEdadSubregistro(ReporteEdadSubregistroPeticion peticion)
        {
            ReporteSubregistroRespuesta respuesta = new ReporteSubregistroRespuesta();
            try
            {
                ReportesBLL reportesBLL = new ReportesBLL();
                respuesta = reportesBLL.ConsultarReporteEdadSubregistro(peticion.ColAnos, peticion.ColMeses, peticion.ColMunicipios);
                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", respuesta.Cabecero);
            }
            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;
        }

        public ReporteSubregistroRespuesta ConsultarReporteEscolaridadSubregistro(ReporteEscolaridadSubregistroPeticion peticion)
        {
            ReporteSubregistroRespuesta respuesta = new ReporteSubregistroRespuesta();
            try
            {
                ReportesBLL reportesBLL = new ReportesBLL();
                respuesta = reportesBLL.ConsultarReporteEscolaridadSubregistro(peticion.ColAnos, peticion.ColMeses, peticion.ColMunicipios);
                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", respuesta.Cabecero);
            }
            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;
        }

        public ReporteSubregistroRespuesta ConsultarReporteNumNacSubregistro(ReporteNumNacSubregistroPeticion peticion)
        {
            ReporteSubregistroRespuesta respuesta = new ReporteSubregistroRespuesta();
            try
            {
                ReportesBLL reportesBLL = new ReportesBLL();
                respuesta = reportesBLL.ConsultarReporteNumNacSubregistro(peticion.ColAnos, peticion.ColMeses, peticion.ColMunicipios);
                AsignarCabeceroRespuesta(0, "Se ejecutó correctamente", respuesta.Cabecero);
            }
            catch (BusinessException e)
            {
                AsignarCabeceroRespuesta(e.Codigo, e.Message, respuesta.Cabecero);
            }
            catch (Exception e)
            {
                AsignarCabeceroRespuesta(-1, "Error interno del Servicio: " + e.Message, respuesta.Cabecero);
            }
            return respuesta;
        }
        #endregion

        #region Metódos Privados
        private void AsignarCabeceroRespuesta(int codigo, string mensaje, CabeceroRespuesta response)
        {
            response.CodigoRespuesta = codigo;
            response.MensajeRespuesta = mensaje;
        }
        #endregion
    }
}
