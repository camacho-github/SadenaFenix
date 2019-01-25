using SadenaFenix.Models.Catalogos.Geografia;
using System.Collections.Generic;

namespace SadenaFenix.Business.Catalogos.Geografia
{
    public class CatMunicipioBusiness
    {
        public List<Municipio> ObtenerTodosLosMuncipios()
        {
            List<Municipio> municipios = new List<Municipio>
            {
                new Municipio(1, "Abasolo"),
                new Municipio(2, "Acuña"),
                new Municipio(3, "Allende")
            };
            return municipios;
        }
    }
}
