using System.Web.Mvc;
using Sadena.Sevices.Catalogos.Geografia;
using Sadena.Transport.Nacimientos.Consultas;
using Sadena.Transport.Nacimientos.Consultas.Comboxes;

namespace SadenaFenix.Controllers.Nacimientos
{

    public class ConsultasController : Controller
    {
        private readonly CatMunicipioFacade CatMunicipioFacade;

        // GET: Consultas
        public ActionResult Consultas()
        {
            ConsultasViewModel model = new ConsultasViewModel();
            model.ComboMunicipios.Municipios.AddRange(CatMunicipioFacade.ObtenerTodosLosMuncipios());
            model.ComboMeses = new MesViewModelIEnumerable();
            model.ComboAnios.Anios.AddRange(null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(MesViewModelIEnumerable model)
        {
            if (ModelState.IsValid)
            {
                var msg = model.MesesSeleccionados;
            }

            // If we got this far, something failed; redisplay form.
            return View(model);
        }


    }
}