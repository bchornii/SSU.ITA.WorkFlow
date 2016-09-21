using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SSU.ITA.WorkFlow.Domain.Services;
using SSU.ITA.WorkFlow.Domain.Services.DTO;

namespace SSU.ITA.WorkFlow.Application.Web.Controllers
{
    public class EmployeesInvitationController : ApiController
    {
        private readonly IInvitationService _service;

        public EmployeesInvitationController(IInvitationService service)
        {
            _service = service;
        }

        public async Task<HttpResponseMessage> SendInvitation(InvitationDto invitation)
        {            
            bool conflict = await _service.SaveInvitedUsers(invitation);
            if (conflict)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);                                   
        }
    }
}
