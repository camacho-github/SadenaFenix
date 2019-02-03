using System.Web.Mvc;

namespace SadenaFenix.Controllers.Georeferenciacion
{
    public class OficialiasController : Controller
    {
        // GET: /Oficialias/Oficialias
        public ActionResult Oficialias()
        {
            return View("~/Views/Georeferenciacion/Oficialias.cshtml");
        }
    }
}
