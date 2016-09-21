using System;
using System.Collections.Generic;
using System.Net;

namespace SSU.ITA.WorkFlow.Application.Web.ExceptionHandlers
{
    public interface IExceptionHandlerCustomizer
    {
        IDictionary<Type, KeyValuePair<HttpStatusCode, string>> Collection { get; }
        HttpStatusCode GetStatusCode<T>() where T : Exception;
        HttpStatusCode GetStatusCode(Type type);
        string GetMessage<T>() where T : Exception;
        string GetMessage(Type type);
        void BindToException<T>(HttpStatusCode statusCode) where T : Exception;
        void BindToException<T>(HttpStatusCode statusCode, string message) where T : Exception;
    }
}
