using SadenaFenix.Business.Georeferenciacion;
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
        #region Métodos Publicos
        public ConsultaOficialiaRespuesta ConsultarOficialias(ConsultaOficialiaPeticion peticion)
        {
            ConsultaOficialiaRespuesta respuesta = new ConsultaOficialiaRespuesta();
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
        #endregion
        #region Métodos Publicos
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