using SadenaFenix.Facade.Nacimientos.Consultas;
using SadenaFenix.Transport.Nacimientos.Consultas.Comboxes;
using System.Web.Mvc;

namespace SadenaFenix.Controllers.Nacimientos
{

    public class ConsultasController : Controller
    {

        // GET: Consultas/Consultar
        [HttpGet]
        public ActionResult Consultar()
        {
            ConsultaFacade ConsultaFacade = new ConsultaFacade();
            return View(ConsultaFacade.ObtenerTodosLosMuncipios());
        }

        // POST: Consultas/Index
        [HttpPost]
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