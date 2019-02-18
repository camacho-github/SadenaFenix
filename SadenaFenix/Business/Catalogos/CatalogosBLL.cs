using SadenaFenix.Commons.Utilerias;
using SadenaFenix.Daos.Catalogos;
using SadenaFenix.Excepcions;
using SadenaFenix.Transport.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SadenaFenix.Business.Catalogos
{
    public class CatalogosBLL
    {
        #region Variables de Instancia
        private CatalogosDAO dao;
        #endregion

        #region Constructor
        public CatalogosBLL()
        {
            dao = new CatalogosDAO();
        }
        #endregion

        #region Métodos Públicos
        public CatalogosSocioEconomicaRespuesta ConsultarCatalogosSocioeconomica()
        {
            CatalogosSocioEconomicaRespuesta response = new CatalogosSocioEconomicaRespuesta
            {
                ColSexo = dao.ConsultaCatSexo(),
                ColEdoCivil = dao.ConsultaCatEdoCivil(),
                ColEscolaridad = dao.ConsultaCatEscolaridad(),
                ColEstatusRegistro = dao.ConsultaCatEstatusRegistro()
            };

            return response;
        }

        public CatalogosGeografiaRespuesta ConsultarCatalogosGeografia()
        {
            CatalogosGeografiaRespuesta response = new CatalogosGeografiaRespuesta
            {
                ColMunicipio = dao.ConsultaCatMunicipio(),
                ColLocalidad = dao.ConsultaCatLocalidad()
            };

            return response;
        }

        public CatalogoMunicipioRespuesta ConsultarCatalogoMunicipio()
        {
            CatalogoMunicipioRespuesta respuesta = new CatalogoMunicipioRespuesta
            {
                ColMunicipio = dao.ConsultaCatPoligonoMunicipio()
            };
            return respuesta;
        }

        public CatalogoLocalidadRespuesta ConsultarCatalogoLocalidad()
        {
            CatalogoLocalidadRespuesta respuesta = new CatalogoLocalidadRespuesta
            {
                ColLocalidad = dao.ConsultaCatLocalidad()
            };
            return respuesta;
        }

        public CatalogoLocalidadRespuesta ConsultaCatLocalidadCoahuila()
        {
            CatalogoLocalidadRespuesta respuesta = new CatalogoLocalidadRespuesta
            {
                ColLocalidad = dao.ConsultaCatLocalidadCoahuila()
            };
            return respuesta;
        }

        

        public ConsultaMesesRespuesta ConsultarMesesXAnio(string anio)
        {
            ConsultaMesesRespuesta consultaMesesRespuesta = new ConsultaMesesRespuesta();
            try
            {
                consultaMesesRespuesta.ColMeses = dao.ConsultarMesesXAnio(anio);
            }
            catch (Exception e)
            {
                Bitacora.Error(e.Message);
                throw new BusinessException("La consulta no fue realizada exitosamente, favor de intentar nuevamente: " + e.Message);
            }

            return consultaMesesRespuesta;
        }

        #endregion
    }
}