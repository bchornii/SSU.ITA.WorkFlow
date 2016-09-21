using System;
using System.Net;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using SSU.ITA.WorkFlow.Application.Web.ExceptionHandlers;
using System.Web.Http.ExceptionHandling;
using SSU.ITA.WorkFlow.Application.Web.Filters;
using System.Data.Entity.Infrastructure;

namespace SSU.ITA.WorkFlow.Application.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            // Suppress another authentication
            config.SuppressDefaultHostAuthentication();

            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));            

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            RegisterExceptionHandler(config);
        }

        public static void RegisterExceptionHandler(HttpConfiguration config)
        {
            IExceptionHandlerCustomizer customizer = ServiceLocator.GetService<ExceptionHandlerCustomizer>();            
            customizer.BindToException<Exception>(HttpStatusCode.InternalServerError);                    
            config.Services.Replace(typeof(IExceptionHandler), new UnhandledExceptionHandler
            {
                Customizer = customizer
            });
        }
    }
}
