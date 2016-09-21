using System;
using System.Collections.Generic;
using System.Net;

namespace SSU.ITA.WorkFlow.Application.Web.ExceptionHandlers
{
    public class ExceptionHandlerCustomizer : IExceptionHandlerCustomizer
    {
        public IDictionary<Type, KeyValuePair<HttpStatusCode, string>> Collection { get; private set; }
        public ExceptionHandlerCustomizer()
        {
            Collection = new Dictionary<Type, KeyValuePair<HttpStatusCode, string>>();
        }
        public HttpStatusCode GetStatusCode(Type type)
        {
            KeyValuePair<HttpStatusCode, string> current;
            return Collection.TryGetValue(type, out current) ? current.Key : HttpStatusCode.InternalServerError;
        }
        public string GetMessage(Type type)
        {
            KeyValuePair<HttpStatusCode, string> current;
            return Collection.TryGetValue(type, out current) ? current.Value : string.Empty;
        }
        public HttpStatusCode GetStatusCode<T>() where T : Exception
        {
            return GetStatusCode(typeof(T));
        }
        public string GetMessage<T>() where T : Exception
        {
            return GetMessage(typeof(T));
        }
        public void BindToException<T>(HttpStatusCode statusCode) where T : Exception
        {
            BindToException<T>(statusCode, string.Empty);
        }
        public void BindToException<T>(HttpStatusCode statusCode, string message) where T : Exception
        {
            Collection[typeof(T)] = new KeyValuePair<HttpStatusCode, string>(statusCode, message);
        }
    }
}