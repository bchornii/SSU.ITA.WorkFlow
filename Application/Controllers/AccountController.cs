using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SSU.ITA.WorkFlow.Domain.Services;
using SSU.ITA.WorkFlow.Domain.Services.DTO;
using System.Net.Mail;

namespace SSU.ITA.WorkFlow.Application.Web.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        public async Task<HttpResponseMessage> Register(RegisterDto registerModel)
        {
            if (await _service.FindUserByEmail(registerModel.Email) == null
                && await _service.FindCompanyByName(registerModel.CompanyName) == null)
            {
                await _service.CreateCompanyAsync(registerModel);
                await _service.CreateUserAsync(registerModel);              
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }
        }
    }
}
