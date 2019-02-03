using System.Web.Mvc;

namespace SadenaFenix.Controllers.Georeferenciacion
{
    public class ModulosHospitalariosController : Controller
    {
        // GET: /ModulosHospitalarios/ModulosHospitalarios
        public ActionResult ModulosHospitalarios()
        {
            return View("~/Views/Georeferenciacion/ModulosHospitalarios.cshtml");
        }
    }
}
