﻿using SadenaFenix.Business.Georeferenciacion;
using SadenaFenix.Business.Usuarios;
using SadenaFenix.Excepcions;
using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Transport.Georeferenciacion;
using SadenaFenix.Transport.Usuarios.Acceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SadenaFenix.Services.Georeferenciacion
{
    public class GeoServicio
    {
        #region Métodos Oficinas
        public ConsultarOficinasRespuesta ConsultarOficinas(ConsultarOficinasPeticion peticion)
        {
            ConsultarOficinasRespuesta respuesta = new ConsultarOficinasRespuesta();
            try
            {
                GeoreferenciacionBLL bll = new GeoreferenciacionBLL();
                respuesta = bll.ConsultarOficinas(peticion.ColMunicipios);
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

        public InsertarOficinaRespuesta InsertarOficina(InsertarOficinaPeticion peticion)
        {
            InsertarOficinaRespuesta respuesta = new InsertarOficinaRespuesta();
            try
            {
                GeoreferenciacionBLL bll = new GeoreferenciacionBLL();
                bool resultado = bll.InsertarOficina(peticion.Oficina);
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

        public ConsultarOficinaRespuesta ConsultarOficina(ConsultarOficinaPeticion peticion)
        {
            ConsultarOficinaRespuesta respuesta = new ConsultarOficinaRespuesta();
            try
            {
                GeoreferenciacionBLL bll = new GeoreferenciacionBLL();
                respuesta = bll.ConsultarOficina(peticion.OId);
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

        public ActualizarOficinaRespuesta EliminarOficina(int oId)
        {
            ActualizarOficinaRespuesta respuesta = new ActualizarOficinaRespuesta();
            try
            {
                GeoreferenciacionBLL bll = new GeoreferenciacionBLL();
                bool resultado = bll.EliminarOficina(oId);
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


        #region Métodos Publicos
        public ConsultaOficialiasRespuesta ConsultarOficialias(ConsultaOficialiasPeticion peticion)
        {
            ConsultaOficialiasRespuesta respuesta = new ConsultaOficialiasRespuesta();
            try
            {
                GeoreferenciacionBLL bll = new GeoreferenciacionBLL();
                respuesta = bll.ConsultarOficialias(peticion.ColMunicipios);
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

        public ConsultaOficialiaRespuesta ConsultarOficialia(ConsultaOficialiaPeticion peticion)
        {
            ConsultaOficialiaRespuesta respuesta = new ConsultaOficialiaRespuesta();
            try
            {
                GeoreferenciacionBLL bll = new GeoreferenciacionBLL();
                respuesta = bll.ConsultarOficialia(peticion.OId);
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

        
        public InsertarOficialiaRespuesta InsertarOficialia(InsertarOficialiaPeticion peticion)
        {
            InsertarOficialiaRespuesta respuesta = new InsertarOficialiaRespuesta();
            try
            {
                GeoreferenciacionBLL bll = new GeoreferenciacionBLL();
                bool resultado = bll.InsertarOficialia(peticion.Oficialia);
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
        public ActualizarOficialiaRespuesta ActualizarOficialia(ActualizarOficialiaPeticion peticion)
        {
            ActualizarOficialiaRespuesta respuesta = new ActualizarOficialiaRespuesta();
            try
            {
                GeoreferenciacionBLL bll = new GeoreferenciacionBLL();
                bool resultado = bll.ActualizarOficialia(peticion.Oficialia);
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

        public ActualizarOficinaRespuesta ActualizarOficina(ActualizarOficinaPeticion peticion)
        {
            ActualizarOficinaRespuesta respuesta = new ActualizarOficinaRespuesta();
            try
            {
                GeoreferenciacionBLL bll = new GeoreferenciacionBLL();
                bool resultado = bll.ActualizarOficina(peticion.Oficina);
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

        public ActualizarOficialiaRespuesta EliminarOficialia(int oId)
        {
            ActualizarOficialiaRespuesta respuesta = new ActualizarOficialiaRespuesta();
            try
            {
                GeoreferenciacionBLL bll = new GeoreferenciacionBLL();
                bool resultado = bll.EliminarOficialia(oId);
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