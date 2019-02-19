using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using SadenaFenix.Commons.Utilerias;
using SadenaFenix.Daos.Georeferenciacion;
using SadenaFenix.Excepcions;
using SadenaFenix.Models.Catalogos.Geografia;
using SadenaFenix.Models.Georeferenciacion;
using SadenaFenix.Transport.Georeferenciacion;

namespace SadenaFenix.Business.Georeferenciacion
{
    public class GeoreferenciacionBLL
    {
        #region Variables de Instancia
        private GeorefenciacionDAO  geoDAO;
        #endregion

        #region Constructor
        public GeoreferenciacionBLL()
        {
            geoDAO = new GeorefenciacionDAO();
        }

        public ConsultaOficialiasRespuesta ConsultarOficialias(Collection<Municipio> colMunicipios)
        {
            IList<string> municipiosLista = new List<string>();
            foreach (Municipio m in colMunicipios)
            {
                municipiosLista.Add(m.MpioId.ToString());
            }
            string municipiosUnion = string.Join(",", municipiosLista);

            DataTable dataTable = geoDAO.ConsultarOficialias(municipiosUnion);

            Collection<Oficialia> colOficialias = new Collection<Oficialia>();

            foreach (DataRow r in dataTable.Rows)
            {
                Oficialia oficialia = new Oficialia
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
                    Longitud = r.Field<string>("Longitud")
                };
                colOficialias.Add(oficialia);
            }

            ConsultaOficialiasRespuesta respuesta = new ConsultaOficialiasRespuesta
            {
                ColOficialia = colOficialias,
                DTOficialia = dataTable
            };

            return respuesta;
        }

        public ConsultaOficialiaRespuesta ConsultarOficialia(int OId)
        {
            ConsultaOficialiaRespuesta consultaOficialiaRespuesta = new ConsultaOficialiaRespuesta();
            try
            {
                Oficialia oficialia = geoDAO.ConsultarOficialia(OId);
                consultaOficialiaRespuesta.Oficialia = oficialia;
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);
                throw new BusinessException("La oficialia no fue obtenida correctamente, favor de intentar nuevamente: " + e.Message);
            }

            return consultaOficialiaRespuesta;
        }

        public bool InsertarOficialia(Oficialia oficialia)
        {
            try
            {
                geoDAO.InsertarOficialia(oficialia);
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);
                throw new BusinessException("La oficialia no fue registrada correctamente, favor de validar los datos: " + e.Message);
            }

            return true;           
        }

        public bool ActualizarOficialia(Oficialia oficialia)
        {
            try
            {
                geoDAO.ActualizarOficialia(oficialia);
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);
                throw new BusinessException("La oficialia no fue actualizada correctamente, favor de validar los datos: " + e.Message);
            }

            return true;
        }
        #endregion

        #region Métodos Públicos


        #endregion

    }
}