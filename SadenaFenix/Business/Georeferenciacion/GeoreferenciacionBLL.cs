using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using SadenaFenix.Daos.Georeferenciacion;
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

        public ConsultaOficialiaRespuesta ConsultarOficialias(Collection<Municipio> colMunicipios)
        {
            IList<string> municipiosLista = new List<string>();
            foreach (Municipio m in colMunicipios)
            {
                municipiosLista.Add(m.MpioId.ToString());
            }
            string municipiosUnion = string.Join(",", municipiosLista);

            DataTable dataTable = geoDAO.ConsultarOficialia(municipiosUnion);

            Collection<Oficialia> colOficialias = new Collection<Oficialia>();

            foreach (DataRow r in dataTable.Rows)
            {
                Oficialia oficialia = new Oficialia
                {
                    OId = r.Field<int>("OId"),
                    OficialiaId = r.Field<int>("OficialiaId"),
                    EdoId = r.Field<int>("EdoId"),
                    MpioId = r.Field<int>("MpioId"),
                    LocId = r.Field<int>("LocId"),
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

            ConsultaOficialiaRespuesta respuesta = new ConsultaOficialiaRespuesta
            {
                ColOficialia = colOficialias,
                DTOficialia = dataTable
            };

            return respuesta;
        }
        #endregion

        #region Métodos Públicos


        #endregion

    }
}