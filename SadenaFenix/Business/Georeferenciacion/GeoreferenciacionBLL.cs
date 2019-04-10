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
                Oficina o = new Oficina
                {
                    OId = r.Field<int>("OId"),
                    OficinaId = r.Field<int>("OficinaId"),
                    TipoId = r.Field<int>("TipoId"),
                    TipoInstitucion = r.Field<string>("TipoInstitucion"),
                    Institucion = r.Field<string>("Institucion"),
                    Latitud = r.Field<string>("Latitud"),
                    Longitud = r.Field<string>("Longitud"),
                    Region = r.Field<string>("Region"),
                    MpioId = r.Field<int>("MpioId"),
                    MpioDesc = r.Field<string>("MpioDesc"),
                    LocId = r.Field<int>("LocId"),
                    LocDesc = r.Field<string>("LocDesc"),
                    Calle = r.Field<string>("Calle"),
                    Numero = r.Field<string>("Numero"),
                    Colonia = r.Field<string>("Colonia"),
                    CP = r.Field<string>("CP"),
                    EntreCalles = r.Field<string>("EntreCalles"),
                    HorarioAtencion = r.Field<string>("HorarioAtencion"),
                    Telefono = r.Field<string>("Telefono"),
                    OficialNombre = r.Field<string>("OficialNombre"),
                    OficialApellidos = r.Field<string>("OficialApellidos"),
                    CorreoE = r.Field<string>("CorreoE"),
                    InvSerLuz = r.Field<byte>("InvSerLuz"),
                    InvSerAgua = r.Field<byte>("InvSerAgua"),
                    InvLocalPropio = r.Field<byte>("InvLocalPropio"),
                    InvSerSanitario = r.Field<byte>("InvSerSanitario"),
                    InvEscritorios = r.Field<byte>("InvEscritorios"),
                    InvSillas = r.Field<byte>("InvSillas"),
                    InvArchiveros = r.Field<byte>("InvArchiveros"),
                    InvCompPriv = r.Field<byte>("InvCompPriv"),
                    InvCompGob = r.Field<byte>("InvCompGob"),
                    InvEscanPriv = r.Field<byte>("InvEscanPriv"),
                    InvEscanGob = r.Field<byte>("InvEscanGob"),
                    InvImpPriv = r.Field<byte>("InvImpPriv"),
                    InvImpGob = r.Field<byte>("InvImpGob"),
                    EquiNet = r.Field<byte>("EquiNet"),
                    EquiTrabNet = r.Field<byte>("EquiTrabNet"),
                    EquiVentExpress = r.Field<byte>("EquiVentExpress"),
                    EquiConDrc = r.Field<byte>("EquiConDrc"),
                    ExpideCurp = r.Field<byte>("ExpideCurp"),
                    ExpideActasForaneas = r.Field<byte>("ExpideActasForaneas")
                };

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


        public ConsultarOficinaRespuesta ConsultarOficina(int OId)
        {
            ConsultarOficinaRespuesta consultarOficinaRespuesta = new ConsultarOficinaRespuesta();
            try
            {
                Oficina oficina = geoDAO.ConsultarOficina(OId);
                consultarOficinaRespuesta.Oficina = oficina;
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);
                throw new BusinessException("La oficina no fue obtenida correctamente, favor de intentar nuevamente: " + e.Message);
            }

            return consultarOficinaRespuesta;
        }

        public bool ActualizarOficina(Oficina oficina)
        {
            try
            {
                geoDAO.ActualizarOficina(oficina);
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);
                throw new BusinessException("La oficina no fue actualizada correctamente, favor de validar los datos: " + e.Message);
            }

            return true;
        }

        public bool EliminarOficina(int oId)
        {
            try
            {
                geoDAO.EliminarOficina(oId);
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);
                throw new BusinessException("La oficina no fue eliminada correctamente, favor de intentar nuevamente: " + e.Message);
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