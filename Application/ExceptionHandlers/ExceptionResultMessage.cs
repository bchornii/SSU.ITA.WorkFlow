using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSU.ITA.WorkFlow.Application.Web.ExceptionHandlers
{
    public class ExceptionResultMessage : IHttpActionResult
    {
        public string Content { get; set; }
        public HttpRequestMessage Request { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(StatusCode)
            {
                Content = new StringContent(Content),
                RequestMessage = Request
            });
        }
    }
}