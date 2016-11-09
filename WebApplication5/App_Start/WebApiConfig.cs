using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Practices.Unity;
using WebApplication5.Interfaces;
using WebApplication5.Models;
using WebApplication5.Services;

namespace WebApplication5
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Unity dependency management
            var container = new UnityContainer();
            container.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());
            container.RegisterType<UserContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IFileService, FileService>(new HierarchicalLifetimeManager());
            container.RegisterType<IRoleService, RoleService>(new HierarchicalLifetimeManager());
            container.RegisterType<ILoginService, LoginService>(new HierarchicalLifetimeManager());
            
            FilterConfig.RegisterGlobalFiltersWithDependencies(GlobalConfiguration.Configuration.Filters, container);
            config.DependencyResolver = new UnityResolver(container);
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // CORS
            var cors = new EnableCorsAttribute("http://localhost:8080", "*", "*");
            config.EnableCors(cors);

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;


        }
    }
}
