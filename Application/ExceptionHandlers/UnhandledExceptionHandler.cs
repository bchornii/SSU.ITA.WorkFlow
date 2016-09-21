using System.Net;
using System.Web.Http.ExceptionHandling;

namespace SSU.ITA.WorkFlow.Application.Web.ExceptionHandlers
{
    public class UnhandledExceptionHandler : ExceptionHandler
    {
        public IExceptionHandlerCustomizer Customizer { get; set; }
        public override void Handle(ExceptionHandlerContext context)
        {
            HttpStatusCode statusCode = Customizer.GetStatusCode(context.Exception.GetType());
            string message = Customizer.GetMessage(context.Exception.GetType());
            message = (message != string.Empty) ? message : context.Exception.Message;
            context.Result = new ExceptionResultMessage
            {
                Content = message,
                Request = context.ExceptionContext.Request,
                StatusCode = statusCode
            };
        }
    }
}