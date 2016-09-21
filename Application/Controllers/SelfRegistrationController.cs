using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SSU.ITA.WorkFlow.Domain.Services;
using SSU.ITA.WorkFlow.Domain.Services.DTO;
using SSU.ITA.WorkFlow.Application.Web.Filters;

namespace SSU.ITA.WorkFlow.Application.Web.Controllers
{
    public class SelfRegistrationController : ApiController
    {
        private readonly ISelfRegistrationService _service;

        public SelfRegistrationController(ISelfRegistrationService service)
        {
            _service = service;
        }

        [ValidateBindingModel]
        public async Task<HttpResponseMessage> SelfRegister(SelfRegistrationDto selfRegistrationModel)
        {
            await _service.SelfRegisterUserAsync(selfRegistrationModel);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}