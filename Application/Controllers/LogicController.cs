using System.Web.Mvc;
using System.Threading.Tasks;
using SSU.ITA.WorkFlow.Domain.Services;

namespace SSU.ITA.WorkFlow.Application.Web.Controllers
{
    public class LogicController : Controller
    {
        private readonly ISelfRegistrationService _service;

        public LogicController(ISelfRegistrationService service) : base()
        {
            _service = service;
        }

        [AllowAnonymous]
        public ActionResult Home()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> SelfRegistration(string token)
        {
            if (token == null)
            {
                return RedirectToAction("SelfRegistrationFail");
            }

            bool exists = await _service.TokenExists(token);

            if (exists)
            {
                return View();
            }
            else
            {
                return RedirectToAction("SelfRegistrationFail");
            }
        }

        [AllowAnonymous]
        public ActionResult SelfRegistrationFail()
        {
            return View();
        }
    }
}