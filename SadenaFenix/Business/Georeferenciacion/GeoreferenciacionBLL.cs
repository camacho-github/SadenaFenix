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
        #endregion

        #region Métodos Públicos Oficinas
        public ConsultarOficinasRespuesta ConsultarOficinas(Collection<Municipio> colMunicipios)
        {
            IList<string> municipiosLista = new List<string>();
            foreach (Municipio m in colMunicipios)
            {
                municipiosLista.Add(m.MpioId.ToString());
            }
            string municipiosUnion = string.Join(",", municipiosLista);

            DataTable dataTable = geoDAO.ConsultarOficinas(municipiosUnion);

            Collection<Oficina> colOficinas = new Collection<Oficina>();

            foreach (DataRow r in dataTable.Rows)
            {
                Oficina o = new Oficina();
                o.OId = r.Field<int>("OId");
                    o.OficinaId = r.Field<int>("OficinaId");
                    o.TipoId = r.Field<int>("TipoId");
                    o.TipoInstitucion = r.Field<string>("TipoInstitucion");
                    o.Institucion = r.Field<string>("Institucion");
                    o.Latitud = r.Field<string>("Latitud");
                    o.Longitud = r.Field<string>("Longitud");
                    o.Region = r.Field<string>("Region");
                    o.MpioId = r.Field<int>("MpioId");
                    o.MpioDesc = r.Field<string>("MpioDesc");
                    o.LocId = r.Field<int>("LocId");
                    o.LocDesc = r.Field<string>("LocDesc");
                    o.Calle = r.Field<string>("Calle");
                    o.Numero = r.Field<string>("Numero");
                    o.Colonia = r.Field<string>("Colonia");
                    o.CP = r.Field<string>("CP");
                    o.EntreCalles = r.Field<string>("EntreCalles");
                    o.HorarioAtencion = r.Field<string>("HorarioAtencion");
                    o.Telefono = r.Field<string>("Telefono");
                    o.OficialNombre = r.Field<string>("OficialNombre");
                    o.OficialApellidos = r.Field<string>("OficialApellidos");
                    o.CorreoE = r.Field<string>("CorreoE");
                    o.InvSerLuz = r.Field<byte>("InvSerLuz");
                    o.InvSerAgua = r.Field<byte>("InvSerAgua");
                    o.InvLocalPropio = r.Field<byte>("InvLocalPropio");
                    o.InvSerSanitario = r.Field<byte>("InvSerSanitario");
                    o.InvEscritorios = r.Field<byte>("InvEscritorios");
                    o.InvSillas = r.Field<byte>("InvSillas");
                    o.InvArchiveros = r.Field<byte>("InvArchiveros");
                    o.InvCompPriv = r.Field<byte>("InvCompPriv");
                    o.InvCompGob = r.Field<byte>("InvCompGob");
                    o.InvEscanPriv = r.Field<byte>("InvEscanPriv");
                    o.InvEscanGob = r.Field<byte>("InvEscanGob");
                    o.InvImpPriv = r.Field<byte>("InvImpPriv");
                    o.InvImpGob = r.Field<byte>("InvImpGob");
                    o.EquiNet = r.Field<byte>("EquiNet");
                    o.EquiTrabNet = r.Field<byte>("EquiTrabNet");
                    o.EquiVentExpress = r.Field<byte>("EquiVentExpress");
                    o.EquiConDrc = r.Field<byte>("EquiConDrc");
                    o.ExpideCurp = r.Field<byte>("ExpideCurp");
                o.ExpideActasForaneas = r.Field<byte>("ExpideActasForaneas");

                colOficinas.Add(o);
            }

            ConsultarOficinasRespuesta respuesta = new ConsultarOficinasRespuesta
            {
                ColOficinas = colOficinas,
                DTOficinas = dataTable
            };

            return respuesta;
        }

        public bool InsertarOficina(Oficina oficina)
        {
            try
            {
                geoDAO.InsertarOficina(oficina);
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);
                throw new BusinessException("La oficina no fue registrada correctamente, favor de validar los datos: " + e.Message);
            }

            return true;
        }

        #endregion Métodos Públicos Oficinas

        #region Métodos Públicos Oficialias

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

        public bool EliminarOficialia(int oId)
        {
            try
            {
                geoDAO.EliminarOficialia(oId);
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);
                throw new BusinessException("La oficialia no fue eliminada correctamente, favor de intentar nuevamente: " + e.Message);
            }

            return true;
        }

        #endregion

    }
}