using Sadena.Models.Catalogos.Geografia;
using System;
using System.Collections.Generic;

namespace Sadena.Business.Catalogos.Geografia
{
    public class CatMunicipioBusiness
    {
        public IList<Municipio> ObtenerTodosLosMuncipios()
        {
            IList<Municipio> municipios = new List<Municipio>
            {
                new Municipio(1, "Abasolo"),
                new Municipio(2, "Acuña"),
                new Municipio(3, "Allende")
            };
            return municipios;
        }
    }
}
