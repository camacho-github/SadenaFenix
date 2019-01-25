﻿using SadenaFenix.Business.Nacimientos.Archivos;
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

namespace Sadena.Services
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
