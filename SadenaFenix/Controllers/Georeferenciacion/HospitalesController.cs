using System.Web.Mvc;

namespace SadenaFenix.Controllers.Georeferenciacion
{
    public class HospitalesController : Controller
    {
        // GET: /Hospitales/Hospitales
        public ActionResult Hospitales()
        {
            return View("~/Views/Georeferenciacion/Hospitales.cshtml");
        }
    }
}
