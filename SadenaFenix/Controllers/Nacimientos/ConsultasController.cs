using Microsoft.AspNetCore.Mvc;
using Sadena.Transporte.Nacimientos.Consultas;
using Sadena.Facade.Catalogos.Geografia;

namespace Sadena.Controllers.Nacimientos.Consultas
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
            model.ComboAnios.Anios.AddRange();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(MesViewModelIEnumerable model)
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